using System;
using EventBus.App.Handlers;
using EventBus.App.Publishers;

namespace EventBus.App
{
    public class EventBus : IEventBus, IDisposable
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISubscriberStore _store;

        private bool _disposing;

        public EventBus(int consumerAmount = 1)
        {
            _disposing = false;
            _store = new EventHandlerStore();
            _eventPublisher = new ProducerConsumerPublisher(consumerAmount, _store);
        }

        private void Publish(Type eventType, IEventData eventData)
        {
            _eventPublisher.Post(new Tuple<Type, IEventData>(eventType, eventData));
        }

        public void Publish<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            Publish(typeof(TEventData), eventData);
        }

        private IDisposable Subscribe(Type eventType, IEventHandler handler)
        {
            return _store.Subscribe(eventType, handler);
        }
        public IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Subscribe(typeof(TEventData), handler);
        }

        public IDisposable Subscribe<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return Subscribe(typeof(TEventData), new ActionEventHandler<TEventData>(action));
        }

        private void Unsubscribe(Type eventType, IEventHandler handler)
        {
            _store.Unsubscribe(eventType, handler);
        }

        public void Unsubscribe<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Type eventType = typeof(TEventData);
            Unsubscribe(eventType, handler);
        }

        public void Unsubscribe<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            _store.Unsubscribe(action);
        }
        public void UnsubscribeAll<TEventData>() where TEventData : IEventData
        {
            UnsubscribeAll(typeof(TEventData));
        }

        private void UnsubscribeAll(Type eventType)
        {
            _store.UnsubscribeAll(eventType);
        }

        public void Dispose()
        {
            if (_disposing == true) return;

            _disposing = true;

            Dispose(_disposing);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _store.Dispose();
                _eventPublisher.Dispose();
            }
        }
    }
}
