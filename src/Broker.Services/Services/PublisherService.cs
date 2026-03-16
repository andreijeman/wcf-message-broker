using Broker.Contracts.Entities;
using Broker.Contracts.Faults;
using Broker.Contracts.Repositories;
using Broker.Contracts.Services;
using Broker.Services.Repositories;
using System.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;
using Message = Broker.Contracts.Data.Message;

namespace Broker.Services.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly ITopicRepository _topicRepository;

        public PublisherService()
        {
            _topicRepository = Container.GetInstance<ITopicRepository>();
        }

        public void Publish(Message message)
        {
            var topic = _topicRepository.GetByName(message.Topic);

            if (topic == null)
                throw new FaultException<PublicationFault>(new PublicationFault { Topic = message.Topic, Description = "Topic not found" });

            foreach (string queue in topic.QueueList)
            {
                var messageQueue = new MessageQueue(queue);
                messageQueue.Send(message);
            }
        }
    }
}