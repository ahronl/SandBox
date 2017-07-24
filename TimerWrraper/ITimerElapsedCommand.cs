using System.Threading;

namespace TimerWrapper
{
    public interface ITimerElapsedCommand
    {       
        void Execute(CancellationToken cToken);
    }
}