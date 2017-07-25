using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimerWrapper
{
    internal interface ITimerEvents: ITimer
    {
        event Action<ITimer, Exception> SafeExceptionEvent;
        event Action<ITimer> DisposedEvent;
    }

    public interface ITimer : IDisposable
    {
        /// <summary>
        /// Internal timer unhandled exception event
        /// </summary>
        event Action<ITimer, Exception> ExceptionEvent;
        /// <summary>
        /// Timer Started Event
        /// </summary>
        event Action<ITimer> StartEvent;
        /// <summary>
        /// Timer Stopped Event
        /// </summary>
        event Action<ITimer> StopEvent;
        /// <summary>
        /// Execution Started Event
        /// </summary>
        event Action<ITimer> ExecutingStartEvent;
        /// <summary>
        /// Execution Ended Event, with execution timespan and result
        /// </summary>
        event Action<ITimer, TimeSpan, bool> ExecutingEndEvent;
        /// <summary>
        /// Execution Cycle has ended event
        /// </summary>
        event Action<ITimer> ExecutionCycleEndEvent;
        /// <summary>
        /// Execution Canceled Event
        /// </summary>
        event Action<ITimer> CanceledEvent;
        /// <summary>
        /// indicates that the instance is started.
        /// </summary>
        bool IsStarted { get; }
        /// <summary>
        /// indicates the timer is executing
        /// </summary>
        bool IsExecuting { get; }
        /// <summary>
        /// instance name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// instance id
        /// </summary>
        int Id { get; }
        /// <summary>
        /// instance unique id
        /// </summary>
        Guid InstanceId { get; }
        /// <summary>
        /// return the managed thread Id may change with every execution
        /// </summary>
        string ManagedThreadId { get; }

        bool IsDisposed { get; }

        /// <summary>
        /// Start timer
        /// </summary>
        void Start();
        /// <summary>
        /// Stop timer
        /// </summary>
        void Stop();
        /// <summary>
        /// Cancel current execution if currently not running the next execution
        /// </summary>
        void CancelCurrentExecution();
        /// <summary>
        /// Runs new execution, throws ObjectDisposedException in case it is disposed
        /// </summary>
        /// <param name="cToken">Execution cancellation token default is None</param>
        /// <returns>Task</returns>
        Task RunNow(CancellationToken cToken = default(CancellationToken));
        /// <summary>
        /// Get the policy settings to reset interval
        /// </summary>
        /// <returns></returns>
        ITimerPolicySettings GetPolicySettings();
    }
}
