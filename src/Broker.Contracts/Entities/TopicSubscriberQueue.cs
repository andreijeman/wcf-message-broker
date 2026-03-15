using System;

namespace Broker.Contracts.Entities
{
    public class TopicSubscriberQueue
    {
        public Guid TopicId { get; set; }
        public Guid SubscriberId { get; set; }
        public string QueuePath { get; set; }
    }
}