using System;

namespace TimerWrapper.Policy
{
    internal class RelativePlusMinimalIntervalProvider : ITimerPolicy
    {
        private TimeSpan _interval;
        private readonly TimeSpan _minimumInterval;

        internal TimeSpan ExecutionTimeSpan { get; private set; }

        public RelativePlusMinimalIntervalProvider(TimeSpan interval, TimeSpan minimumInterval)
        {
            _interval = interval;
            _minimumInterval = minimumInterval;           
        }
        public void SetNewInterval(TimeSpan interval)
        {
            _interval = interval;
        }
        public TimeSpan GetInterval(DateTime runStartUtcTime)
        {
            ExecutionTimeSpan = DateTime.UtcNow.Subtract(runStartUtcTime);

            TimeSpan diff = _interval.Subtract(ExecutionTimeSpan);

            return _minimumInterval + diff;
        }        
    }
}
