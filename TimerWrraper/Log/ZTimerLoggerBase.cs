using System;

namespace TimerWrapper.Log
{
    internal abstract class ZTimerLoggerBase : IDisposable
    {
        private readonly ITimerEvents _timer;
        private readonly ZTimerLoggerBase _ztimerLoggerBase;
        private bool _disposing;

        protected ZTimerLoggerBase(ITimerEvents timer, ZTimerLoggerBase ztimerLoggerBase = null)
        {
            _timer = timer;
            _ztimerLoggerBase = ztimerLoggerBase;
            _timer.StartEvent += LogStartEvent;
            _timer.StopEvent += LogStopEvent;
            _timer.CanceledEvent += LogCanceledEvent;
            _timer.ExceptionEvent += LogExceptionEvent;
            _timer.ExecutingEndEvent += LogExecutingEndEvent;
            _timer.ExecutingStartEvent += LogExecutingStartEvent;
            _timer.SafeExceptionEvent += LogExceptionEvent;
            _timer.DisposedEvent += DisposeEvents;
        }

        protected virtual void LogExecutingStartEvent(ITimer sender)
        {
            _ztimerLoggerBase?.LogExecutingStartEvent(sender);
        }

        protected virtual void LogExecutingEndEvent(ITimer sender, TimeSpan executionTimeSpan, bool completedsuccess)
        {
            _ztimerLoggerBase?.LogExecutingEndEvent(sender, executionTimeSpan, completedsuccess);
        }

        protected virtual void LogExceptionEvent(ITimer sender, Exception ex)
        {
            _ztimerLoggerBase?.LogExceptionEvent(sender, ex);
        }

        protected virtual void LogCanceledEvent(ITimer sender)
        {
            _ztimerLoggerBase?.LogCanceledEvent(sender);
        }

        protected virtual void LogStopEvent(ITimer sender)
        {
            _ztimerLoggerBase?.LogStopEvent(sender);
        }

        protected virtual void LogStartEvent(ITimer sender)
        {
            _ztimerLoggerBase?.LogStartEvent(sender);
        }

        protected virtual void DisposeEvents(ITimer sender)
        {
            _ztimerLoggerBase?.DisposeEvents(sender);
        }

        public void Dispose()
        {
            if (_disposing) return;

            _disposing = true;

            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _timer.StartEvent -= LogStartEvent;
                _timer.StopEvent -= LogStopEvent;
                _timer.CanceledEvent -= LogCanceledEvent;
                _timer.ExceptionEvent -= LogExceptionEvent;
                _timer.ExecutingEndEvent -= LogExecutingEndEvent;
                _timer.ExecutingStartEvent -= LogExecutingStartEvent;
                _timer.SafeExceptionEvent -= LogExceptionEvent;
                _timer.DisposedEvent -= DisposeEvents;
            }
        }
    }
}