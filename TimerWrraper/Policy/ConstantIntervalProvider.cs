using System;

namespace TimerWrapper.Policy
{
    internal class ConstantIntervalProvider : ITimerPolicy
    {
        private TimeSpan _interval;       

        public ConstantIntervalProvider(TimeSpan interval)
        {
            _interval = interval;
        }

        public void SetNewInterval(TimeSpan interval)
        {
            _interval = interval;
        }

        public TimeSpan GetInterval(DateTime runStartUtcTime)
        {
            if (TimeSpan.Zero < _interval)
            {
                return _interval;
            }

            return TimeSpan.FromMilliseconds(10);
        }       
    }
}