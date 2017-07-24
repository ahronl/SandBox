using System;
using System.Threading;

namespace TimerWrapper.CommandAdapters
{    
    internal class ActionTimerElapsedCommand : ITimerElapsedCommand
    {
        private readonly Action _command;

        public ActionTimerElapsedCommand(Action command)
        {
            _command = command;
        }
        public void Execute(CancellationToken cToken)
        {
            if (cToken.IsCancellationRequested)
            {
                return;
            }

            _command();
        }
    }
}
