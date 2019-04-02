using System;

namespace TimerWrapper.Log
{
    internal class TimerEventListener : TimerLoggerBase
    {
        public TimerEventListener(ITimerEvents timer, TimerLoggerBase timerLoggerBase = null)
            : base(timer, timerLoggerBase)
        {
        }

        protected override void LogExecutingStartEvent(ITimer sender)
        {
            base.LogExecutingStartEvent(sender);
        }

        protected override void LogExceptionEvent(ITimer sender, Exception ex)
        {
            base.LogExceptionEvent(sender, ex);
        }

        protected override void DisposeEvents(ITimer sender)
        {
            base.DisposeEvents(sender);
        }

        protected override void LogStopEvent(ITimer sender)
        {
            base.LogStopEvent(sender);
        }
    }
}
