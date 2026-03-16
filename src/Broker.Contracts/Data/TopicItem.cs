using System.Runtime.Serialization;

namespace Broker.Contracts.Data
{
    [DataContract]
    public class TopicItem
    {
        [DataMember]
        public string Name { get; set; }
    }
}
