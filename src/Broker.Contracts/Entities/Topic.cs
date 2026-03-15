using System;
using System.Collections.Generic;

namespace Broker.Contracts.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TopicSubscriberQueue> QueueList { get; set; }
    }
}