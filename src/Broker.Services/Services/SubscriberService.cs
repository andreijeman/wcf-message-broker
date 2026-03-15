using Broker.Contracts;
using Broker.Contracts.data;
using Broker.Contracts.Data;
using Broker.Contracts.Repositories;
using System;
using System.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Service.Services
{
    public class SubscriberService : ISubscriber
    {
        private readonly ITopicRepository _topicRepository;

        public SubscriberService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<SubscriptionResponse> Subscribe(string topicName)
        {
            if (!await _topicRepository.ExistsByName(topicName))
                throw new FaultException<SubscriptionFault>(new SubscriptionFault { Topic = topicName, Description = "Topic not found" });

            // todo: use real subscriber id after adding auth
            var queuePath = Configuration.MsqmBasePath + topicName + Guid.NewGuid().ToString();

            if (MessageQueue.Exists(Configuration.MsqmBasePath + topicName))
                throw new FaultException<SubscriptionFault>(new SubscriptionFault { Topic = topicName, Description = "Already subscribed" });

            try
            {
                MessageQueue.Create(@".\Private$\SomeTestName");
            }
            catch
            {
                throw new FaultException<SubscriptionFault>(new SubscriptionFault { Topic = topicName, Description = "Subscription failed" });
            }

            return new SubscriptionResponse { TopicName = topicName, QueuePath = queuePath };
        }
    }
}