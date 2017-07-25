namespace EventBus.App
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