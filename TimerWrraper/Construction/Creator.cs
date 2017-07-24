using System;
using System.Threading;
using TimerWrapper.CommandAdapters;
using TimerWrapper.Impl;
using TimerWrapper.Log;
using TimerWrapper.Policy;

namespace TimerWrapper.Construction
{
    public class Creator : ICanAddCommand, ICanAddElapsed, ICanAddPolicy, ICanAddRelativeInterval, ICanAddName
    {
        private ITimerElapsedCommand _command;
        private ITimerPolicy _policy;

        public static ICanAddCommand Create()
        {            
            return new Creator();
        }

        public ICanAddElapsed WithAction(Action action)
        {
            _command = new ActionTimerElapsedCommand(action);
            return this;
        }

        public ICanAddElapsed WithAction(ITimerElapsedCommand command)
        {
            _command = command;
            return this;
        }

        public ICanAddElapsed WithAction(Action<CancellationToken> command)
        {
            _command = new CancelableActionTimerCommand(command);
            return this;
        }

        public ICanAddPolicy IntervalPolicy()
        {
            return this;
        }

        public ICanAddName Constant(TimeSpan interval)
        {
            var intervalPolicy = new ConstantIntervalProvider(interval);
            return Policy(intervalPolicy);
        }

        public ICanAddRelativeInterval Relative()
        {
            return this;
        }

        public ICanAddName Interval(TimeSpan interval)
        {
            var intervalPolicy = new RelativeIntervalProvider(interval);
            return Policy(intervalPolicy);
        }

        public ICanAddName IntervalRange(TimeSpan interval, TimeSpan minimumInterval)
        {
            var intervalPolicy = new RelativeIntervalProvider(interval, minimumInterval);
            return Policy(intervalPolicy);
        }

        public ICanAddName IntervalPlusMinimal(TimeSpan interval, TimeSpan minimal)
        {
            var intervalPolicy = new RelativePlusMinimalIntervalProvider(interval, minimal);
            return Policy(intervalPolicy);
        }

        public ICanAddName Policy(ITimerPolicy intervalPolicy)
        {
            _policy = intervalPolicy;
            return this;
        }

        public ITimer WithTimerName(string name)
        {
            var timer = new TimerBasedImpl(_command, _policy, name);

            CreateZTimerEventListeners(timer);

            return timer;
        }

        private void CreateZTimerEventListeners(TimerBasedImpl timer)
        {
            var timerLogger = new ZTimerLogger(timer);
            new ZTimerEventListener(timer, timerLogger);
        }
    }
}
