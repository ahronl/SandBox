using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus.App.Handlers
{
    internal class EventHandlerStore : ISubscriberStore, IDisposable
    {
        private readonly object _locker;
        private readonly Dictionary<Type, List<IEventHandler>> _eventHandlers;

        private bool _disposing;

        public EventHandlerStore()
        {
            _disposing = false;
            _locker = new object();
            _eventHandlers = new Dictionary<Type, List<IEventHandler>>();
        }

        public IEnumerable<IEventHandler> GetHandlers(Type eventType)
        {
            var eventHandlers = new List<IEventHandler>();

            lock (_locker)
            {
                foreach (var handlerFactory in _eventHandlers
                    .Where(hf => ShouldPublishEventForHandler(eventType, hf.Key)))
                {
                    eventHandlers.AddRange(handlerFactory.Value);
                }

                return eventHandlers.ToArray();
            }
        }

        private bool ShouldPublishEventForHandler(Type eventType, Type handlerType)
        {
            //Should trigger same type
            if (handlerType == eventType)
            {
                return true;
            }

            //Should trigger for inherited types
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }

        public IDisposable Subscribe(Type eventType, IEventHandler handler)
        {
            lock (_locker)
            {
                if (_eventHandlers.ContainsKey(eventType) == false)
                {
                    _eventHandlers[eventType] = new List<IEventHandler>();
                }

                _eventHandlers[eventType].Add(handler);

                return new HandlerDisposer(this, eventType, handler);
            }
        }

        public void Unsubscribe(Type eventType, IEventHandler handler)
        {
            lock (_locker)
            {
                if (_eventHandlers.ContainsKey(eventType) == false)
                {
                    return;
                }

                _eventHandlers[eventType].Remove(handler);
            }
        }
        public void Unsubscribe<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            Type eventType = typeof(TEventData);

            lock (_locker)
            {
                if (_eventHandlers.ContainsKey(eventType) == false)
                {
                    return;
                }

                _eventHandlers[eventType].RemoveAll(
                    handler =>
                    {
                        var actionHandler = handler as ActionEventHandler<TEventData>;
                        if (actionHandler == null)
                        {
                            return false;
                        }

                        return actionHandler.Action == action;
                    });
            }
        }

        public void UnsubscribeAll(Type eventType)
        {
            lock (_locker)
            {
                if (_eventHandlers.ContainsKey(eventType) == false)
                {
                    return;
                }

                _eventHandlers.Remove(eventType);
            }
        }

        public void Dispose()
        {
            if (_disposing) return;

            _disposing = true;

            Dispose(_disposing);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_disposing)
            {
                _eventHandlers.Clear();
            }
        }
    }
}