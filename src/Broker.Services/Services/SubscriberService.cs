using Broker.Contracts.Data;
using Broker.Contracts.Faults;
using Broker.Contracts.Repositories;
using Broker.Contracts.Services;
using Broker.Services.Repositories;
using System;
using System.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Services.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ITopicRepository _topicRepository;

        public SubscriberService()
        {
            _topicRepository = Container.GetInstance<ITopicRepository>();
        }

        public SubscriberService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public SubscriptionResponse Subscribe(string topicName)
        {
            var topic = _topicRepository.GetByName(topicName);

            if (topic == null)
                throw new FaultException<SubscriptionFault>(new SubscriptionFault { Topic = topicName, Description = "Topic not found" });

            // todo: use real subscriber id after adding auth
            var queuePath = Configuration.MsqmBasePath + topicName + Guid.NewGuid().ToString();

            if (MessageQueue.Exists(queuePath))
                throw new FaultException<SubscriptionFault>(new SubscriptionFault { Topic = topicName, Description = "Already subscribed" });

            topic.QueueList.Add(queuePath);

            try
            {
                MessageQueue.Create(queuePath);
            }
            catch
            {
                throw new FaultException<SubscriptionFault>(new SubscriptionFault { Topic = topicName, Description = "Subscription failed" });
            }

            return new SubscriptionResponse { TopicName = topicName, QueuePath = queuePath };
        }
    }
}