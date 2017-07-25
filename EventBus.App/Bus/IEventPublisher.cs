using System;

namespace EventBus.App.Bus
{
    internal interface IEventPublisher:IDisposable
    {
        void Post(Tuple<Type, IEventData> item);
    }
}