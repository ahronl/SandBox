using System;

namespace TimerWrapper
{
    public interface ITimerPolicySettings
    {
        void SetNewInterval(TimeSpan interval);
    }
}
