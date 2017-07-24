using System;

namespace TimerWrapper.Construction
{
    public interface ICanAddRelativeInterval
    {
        /// <summary>
        /// Relative interval, relative to execution start time
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        ICanAddName Interval(TimeSpan interval);
        /// <summary>
        /// Relative interval, relative to execution start time
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="minimumInterval">minimum time to wait</param>
        /// <returns></returns>
        ICanAddName IntervalRange(TimeSpan interval, TimeSpan minimumInterval);
        /// <summary>
        /// Relative interval, relative to execution start time + minimum interval
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="minimal">minimum time to append</param>
        /// <returns></returns>
        ICanAddName IntervalPlusMinimal(TimeSpan interval, TimeSpan minimal);
    }
}
