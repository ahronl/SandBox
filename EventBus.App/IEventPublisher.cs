using System;

namespace EventBus.App
{
    internal interface IEventPublisher:IDisposable
    {
        void Post(Tuple<Type, IEventData> item);
    }
}