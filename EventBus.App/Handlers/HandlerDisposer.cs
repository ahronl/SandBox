using System;

namespace EventBus.App.Handlers
{
    internal class HandlerDisposer : IDisposable
    {
        private readonly ISubscriberStore _store;
        private readonly Type _eventType;
        private readonly IEventHandler _handler;

        public HandlerDisposer(ISubscriberStore store, Type eventType, IEventHandler handler)
        {
            _store = store;
            _eventType = eventType;
            _handler = handler;
        }

        public void Dispose()
        {
            _store.Unsubscribe(_eventType, _handler);
        }
    }
}

