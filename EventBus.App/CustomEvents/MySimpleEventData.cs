using EventBus.App.Bus;

namespace EventBus.App.CustomEvents
{
    internal class MySimpleEventData : EventData
    {
        public MySimpleEventData(string msg)
        {
            Message = msg;
        }

        public string Message { get; set; }
    }
}