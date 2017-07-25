using System;
using System.Runtime.Serialization;

namespace EventBus.App
{
    [Serializable]
    public abstract class EventData : IEventData
    {
        [DataMember]
        public DateTime EventTime { get; set; }
       
        protected EventData()
        {
            EventTime = DateTime.UtcNow;
        }
    }
}