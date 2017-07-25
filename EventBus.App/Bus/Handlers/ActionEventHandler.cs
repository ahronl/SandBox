using System;

namespace EventBus.App.Bus.Handlers
{
    public class ActionEventHandler<TEventData> : IEventHandler<TEventData>
    {
        public Action<TEventData> Action { get; private set; }

        public ActionEventHandler(Action<TEventData> action)
        {
            Action = action;
        }

        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}