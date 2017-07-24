using System;

namespace TimerWrapper.Policy
{
    internal class RelativeIntervalProvider : ITimerPolicy
    {
        private TimeSpan _interval;
        private readonly TimeSpan _minimumInterval;        

        internal TimeSpan ExecutionTimeSpan { get; set; }

        public RelativeIntervalProvider(TimeSpan interval)
            : this(interval, TimeSpan.Zero)
        {
        }

        public RelativeIntervalProvider(TimeSpan interval, TimeSpan minimumInterval)
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

            if (diff > _minimumInterval)
            {
                return diff;
            }

            return _minimumInterval;
        }
    }
}