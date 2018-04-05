using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TimerWrapper.Impl
{
    internal class TimerBasedImpl : ITimer, ITimerEvents
    {
        public int Id
        {
            get; private set;
        }

        public bool IsExecuting => _isExecuting;

        public bool IsDisposed => _disposed;

        public bool IsStarted
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public Guid InstanceId
        {
            get; private set;
        }

        public string ManagedThreadId { get; private set; }

        public event Action<ITimer> CanceledEvent;
        public event Action<ITimer, Exception> ExceptionEvent;
        public event Action<ITimer, TimeSpan, bool> ExecutingEndEvent;
        public event Action<ITimer> ExecutingStartEvent;
        public event Action<ITimer> StartEvent;
        public event Action<ITimer> StopEvent;
        public event Action<ITimer, Exception> SafeExceptionEvent;
        public event Action<ITimer> ExecutionCycleEndEvent;
        public event Action<ITimer> DisposedEvent;

        private const double Tolerance = 15;
        private static int _instanceIdIndex = -1;

        private System.Timers.Timer _timer;
        private readonly ITimerElapsedCommand _command;
        private readonly ITimerPolicy _intervalPolicy;
        private readonly object _executionLock;

        private CancellationTokenSource _cancellationTokenSource;
        private volatile bool _isExecuting;
        private volatile bool _disposed;
        private DateTime _executeStartTime;

        public TimerBasedImpl(ITimerElapsedCommand command, ITimerPolicy intervalPolicy, string name)
        {
            _executionLock = new object();
            _timer = new System.Timers.Timer();
            InstanceId = Guid.NewGuid();
            _intervalPolicy = intervalPolicy;
            _disposed = _isExecuting = false;
            _command = command;
            Name = name;
            _timer.Elapsed += ElapsedCommand;
            ManagedThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Id = Interlocked.Increment(ref _instanceIdIndex);
        }

        private void ElapsedCommand(object e, ElapsedEventArgs args)
        {
            try
            {
                ManagedThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
                CancellationToken cToken = GetToken();
                ElapsedCommand(cToken);
                Notify(ExecutionCycleEndEvent);
            }
            catch (Exception ex)
            {
                Notify(SafeExceptionEvent, ex);
            }
        }
        private void ElapsedCommand(CancellationToken cToken)
        {
            _executeStartTime = DateTime.UtcNow;
            ExecuteCommand(_command, cToken);

            CreateCancellationTokenSource();
            ReinitializeTimer(_executeStartTime);
        }       

        private CancellationToken GetToken()
        {            
            return _cancellationTokenSource.Token;
        }
        private void CreateCancellationTokenSource()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
        private void ReinitializeTimer(DateTime executeStartTime)
        {
            if (_disposed)
            {
                return;
            }

            //If timer not started and executed using RunNow() do not start the timer
            if (!IsStarted)
            {
                return;
            }

            //AutoReset: Gets or sets a value indicating whether the Timer should raise the Elapsed event each time the specified interval elapses or only after the first time it elapses.
            //https://msdn.microsoft.com/en-us/library/vstudio/system.timers.timer.autoreset(v=vs.100).aspx

            //If Enabled and AutoReset are both set to false, and the timer has previously been enabled, setting the Interval property causes the Elapsed event to be raised once, as if the Enabled property had been set to true. 
            //To set the interval without raising the event, you can temporarily set the AutoReset property to true.
            //https://msdn.microsoft.com/en-us/library/vstudio/system.timers.timer.interval(v=vs.100).aspx
           
            _timer.AutoReset = true;
            _timer.Interval = GetInterval(executeStartTime);
            _timer.AutoReset = false;

            _timer.Start();
        }

        private double GetInterval(DateTime executeStartTime)
        {
            TimeSpan interval = _intervalPolicy.GetInterval(executeStartTime);

            double millisecondsInterval = interval.TotalMilliseconds;

            if (millisecondsInterval > Tolerance)
            {
                return millisecondsInterval;
            }
            else
            {
                return Tolerance;
            }
        }
      
        private void ExecuteCommand(ITimerElapsedCommand cmd, CancellationToken token)
        {
            lock (_executionLock)
            {
                if (_isExecuting)
                {                    
                    return;
                }

                _isExecuting = true;
            }

            Stopwatch sp = new Stopwatch();
            bool compleatedsuccess = false;

            try
            {
                Notify(ExecutingStartEvent);

                sp.Start();

                cmd.Execute(token);

                sp.Stop();

                compleatedsuccess = true;
            }
            catch (Exception ex)
            {
                Notify(ExceptionEvent, ex);
            }
            finally
            {
                Notify(ExecutingEndEvent, sp.Elapsed, compleatedsuccess);

                lock (_executionLock)
                {
                    _isExecuting = false;
                }
            }
        }
       
        public void CancelCurrentExecution()
        {
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested == true)
            {
                return;
            }

            _cancellationTokenSource.Cancel();
            Notify(CanceledEvent);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(_disposed) return;

            Notify(DisposedEvent);

            Stop();

            _disposed = true;

            if (disposing)
            {
                DisposeOfTimer();
                _cancellationTokenSource?.Dispose();
            }
        }
        private void DisposeOfTimer()
        {
            if (_timer == null) return;
            
            _timer.Elapsed -= ElapsedCommand;
            _timer.Dispose();
            _timer = null;            
        }
        public ITimerPolicySettings GetPolicySettings()
        {
            return _intervalPolicy;
        }       
        public Task RunNow(CancellationToken cToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (IsStarted == false) return Task.FromResult(0);

            return Task.Factory.StartNew(x => ElapsedCommand(cToken), cToken);
        }
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(string.Format($"can not run now a disposed timer Id: {Id},Name: {Name},ZTimerInstanceId: {InstanceId}"));
            }
        }
        public void Start()
        {
            ThrowIfDisposed();

            IsStarted = true;
            CreateCancellationTokenSource();
            _executeStartTime = DateTime.UtcNow;
            ReinitializeTimer(_executeStartTime);
            Notify(StartEvent);
        }
        public void Stop()
        {
            ThrowIfDisposed();

            IsStarted = false;
            _timer.Stop();
            Notify(StopEvent);
        }
        private void Notify(Action<ITimer> handler)
        {
            var threadSafeHandler = handler;

            if (threadSafeHandler != null)
            {
                threadSafeHandler(this);
            }
        }
        private void Notify<T>(Action<ITimer, T> handler, T arg)
        {
            var threadSafeHandler = handler;

            if (threadSafeHandler != null)
            {
                threadSafeHandler(this, arg);
            }
        }
        private void Notify<T1, T2>(Action<ITimer, T1, T2> handler, T1 arg1, T2 arg2)
        {
            var threadSafeHandler = handler;

            if (threadSafeHandler != null)
            {
                threadSafeHandler(this, arg1, arg2);
            }
        }
    }
}
