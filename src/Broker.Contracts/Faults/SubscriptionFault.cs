using System.Runtime.Serialization;

namespace Broker.Contracts.Faults
{
    [DataContract]
    public class SubscriptionFault
    {
        [DataMember]
        public string Topic { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}