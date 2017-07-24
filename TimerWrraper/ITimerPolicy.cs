using System;

namespace TimerWrapper
{
    public interface ITimerPolicy : ITimerPolicySettings
    {
        TimeSpan GetInterval(DateTime runStartUtcTime);       
    }
}