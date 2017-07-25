using System;
using System.Collections.Generic;
using EventBus.App.Bus.Handlers;

namespace EventBus.App.Bus
{
    internal interface ISubscriberStore:IDisposable
    {
        IEnumerable<IEventHandler> GetHandlers(Type eventType);
        IDisposable Subscribe(Type eventType, IEventHandler handler);
        void Unsubscribe(Type eventType, IEventHandler handler);
        void Unsubscribe<TEventData>(Action<TEventData> action) where TEventData : IEventData;
        void UnsubscribeAll(Type eventType);
    }
}