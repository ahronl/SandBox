using System;
using NLog;

namespace TimerWrapper.Log
{
    internal class ZTimerLogger : ZTimerLoggerBase
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        public ZTimerLogger(ITimerEvents timer)
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
            s_logger.Error(ex, $"ZTimer encounter an error Timer Id : {sender.Id} Timer name: {sender.Name} Timer GUID: {sender.ZTimerInstanceId}");
        }

        protected override void LogCanceledEvent(ITimer sender)
        {
            s_logger.Info($"ZTimer canceled execution Timer Id :{ sender.Id}, Timer name : {sender.Name} Timer GUID: {sender.ZTimerInstanceId}");
        }

        protected override void LogStopEvent(ITimer sender)
        {
            s_logger.Info($"ZTimer stopped Timer Id :{sender.Id}, Timer name : {sender.Name} Timer GUID: {sender.ZTimerInstanceId}");
        }

        protected override void LogStartEvent(ITimer sender)
        {
            s_logger.Info($"ZTimer started Timer Id :{sender.Id}, Timer name : {sender.Name} Timer GUID: {sender.ZTimerInstanceId}");
        }
    }
}
