using System;
using System.Threading;

namespace TimerWrapper.Construction
{
    public interface ICanAddCommand
    {
        /// <summary>
        /// Action to invoke on timer elapsed
        /// </summary>
        /// <param name="action"></param>
        /// <returns>IZTimer instance</returns>
        ICanAddElapsed WithAction(Action action);
        /// <summary>
        /// Command to invoke on timer elapsed
        /// </summary>
        /// <param name="command">Cancelable ICommand</param>
        /// <returns>IZTimer instance</returns>
        ICanAddElapsed WithAction(ITimerElapsedCommand command);

        /// <summary>
        /// Action to invoke on timer elapsed
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        ICanAddElapsed WithAction(Action<CancellationToken> command);
    }
}
