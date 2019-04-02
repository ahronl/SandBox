using System;
using NLog;

namespace TimerWrapper.Log
{
    internal class TimerLogger : TimerLoggerBase
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        public TimerLogger(ITimerEvents timer)
            : base(timer)
        {
        }

        protected override void LogExecutingStartEvent(ITimer sender)
        {
        }

        protected override void LogExecutingEndEvent(ITimer sender, TimeSpan executionTimeSpan, bool completedsuccess)
        {
        }

        protected override void LogExceptionEvent(ITimer sender, Exception ex)
        {
            s_logger.Error(ex, $"Timer encounter an error Timer Id : {sender.Id} Timer name: {sender.Name} Timer GUID: {sender.InstanceId}");
        }

        protected override void LogCanceledEvent(ITimer sender)
        {
            s_logger.Info($"Timer canceled execution Timer Id :{ sender.Id}, Timer name : {sender.Name} Timer GUID: {sender.InstanceId}");
        }

        protected override void LogStopEvent(ITimer sender)
        {
            s_logger.Info($"Timer stopped Timer Id :{sender.Id}, Timer name : {sender.Name} Timer GUID: {sender.InstanceId}");
        }

        protected override void LogStartEvent(ITimer sender)
        {
            s_logger.Info($"Timer started Timer Id :{sender.Id}, Timer name : {sender.Name} Timer GUID: {sender.InstanceId}");
        }
    }
}
