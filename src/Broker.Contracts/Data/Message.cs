using System.Runtime.Serialization;

namespace Broker.Contracts.Data
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Event { get; set; }

        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public string Topic { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}