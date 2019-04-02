using System;

namespace TimerWrapper.Log
{
    internal abstract class TimerLoggerBase : IDisposable
    {
        private readonly ITimerEvents _timer;
        private readonly TimerLoggerBase _timerLoggerBase;
        private bool _disposing;

        protected TimerLoggerBase(ITimerEvents timer, TimerLoggerBase timerLoggerBase = null)
        {
            _timer = timer;
            _timerLoggerBase = timerLoggerBase;
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
            _timerLoggerBase?.LogExecutingStartEvent(sender);
        }

        protected virtual void LogExecutingEndEvent(ITimer sender, TimeSpan executionTimeSpan, bool completedsuccess)
        {
            _timerLoggerBase?.LogExecutingEndEvent(sender, executionTimeSpan, completedsuccess);
        }

        protected virtual void LogExceptionEvent(ITimer sender, Exception ex)
        {
            _timerLoggerBase?.LogExceptionEvent(sender, ex);
        }

        protected virtual void LogCanceledEvent(ITimer sender)
        {
            _timerLoggerBase?.LogCanceledEvent(sender);
        }

        protected virtual void LogStopEvent(ITimer sender)
        {
            _timerLoggerBase?.LogStopEvent(sender);
        }

        protected virtual void LogStartEvent(ITimer sender)
        {
            _timerLoggerBase?.LogStartEvent(sender);
        }

        protected virtual void DisposeEvents(ITimer sender)
        {
            _timerLoggerBase?.DisposeEvents(sender);
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
