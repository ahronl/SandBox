using System;
using System.Reflection;

namespace EventBus.App.Handlers
{
    internal static class EventHandlerExtention
    {
        public static void Invoke(this IEventHandler eventHandler, IEventData eventData, Type eventType)
        {
            if (eventHandler == null)
            {
                return;
            }

            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            try
            {
                handlerType
                    .GetMethod("HandleEvent", BindingFlags.Public | BindingFlags.Instance, null, new[] {eventType},
                        null)
                    .Invoke(eventHandler, new object[] {eventData});
            }
            catch (TargetInvocationException)
            {
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}