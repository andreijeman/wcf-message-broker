using System;
using System.Collections.Generic;

namespace Broker.Contracts.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public ICollection<TopicSubscriberQueue> QueueList { get; set; } = new List<TopicSubscriberQueue>();
        public ICollection<string> QueueList { get; set; } = new List<string>();
    }
}