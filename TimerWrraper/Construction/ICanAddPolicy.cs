using System;

namespace TimerWrapper.Construction
{
    public interface ICanAddPolicy
    {
        /// <summary>
        /// Constant interval, won't take in to account command execution time
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        ICanAddName Constant(TimeSpan interval);
        /// <summary>
        /// Relative interval, relative to execution start time
        /// </summary>        
        /// <returns></returns>
        ICanAddRelativeInterval Relative();       
        /// <summary>
        /// Set custom policy
        /// </summary>
        /// <param name="intervalPolicy"></param>
        /// <returns></returns>
        ICanAddName Policy(ITimerPolicy intervalPolicy);
    }    
}
