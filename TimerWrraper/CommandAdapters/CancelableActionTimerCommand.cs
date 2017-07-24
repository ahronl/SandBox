using System;
using System.Threading;

namespace TimerWrapper.CommandAdapters
{
    internal class CancelableActionTimerCommand : ITimerElapsedCommand
    {
        private readonly Action<CancellationToken> _command;

        public CancelableActionTimerCommand(Action<CancellationToken> command)
        {
            _command = command;
        }
        public void Execute(CancellationToken cToken)
        {
            if (cToken.IsCancellationRequested)
            {
                return;
            }

            _command(cToken);
        }
    }
}
