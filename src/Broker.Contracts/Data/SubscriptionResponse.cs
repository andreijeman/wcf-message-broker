using System.Runtime.Serialization;

namespace Broker.Contracts.Data
{
    [DataContract]
    public class SubscriptionResponse
    {
        [DataMember]
        public string TopicName { get; set; }

        [DataMember]
        public string QueuePath { get; set; }
    }
}