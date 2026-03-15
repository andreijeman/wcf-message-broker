using Broker.Contracts.Entities;
using Broker.Contracts.Faults;
using Broker.Contracts.Repositories;
using Broker.Contracts.Services;
using System.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;
using Message = Broker.Contracts.Data.Message;

namespace Broker.Services.Services
{
    public class PublisherService : IPublisher
    {
        private readonly ITopicRepository _topicRepository;

        public PublisherService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task Publish(Message message)
        {
            var topic = await _topicRepository.GetByName(message.Topic);

            if (topic == null)
                throw new FaultException<PublicationFault>(new PublicationFault { Topic = message.Topic, Description = "Topic not found" });

            foreach (TopicSubscriberQueue queue in topic.QueueList)
            {
                var messageQueue = new MessageQueue(queue.QueuePath);
                messageQueue.Send(message);
            }
        }
    }
}