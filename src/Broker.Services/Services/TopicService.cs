using Broker.Contracts.Data;
using Broker.Contracts.Repositories;
using Broker.Contracts.Services;
using System.Collections.Generic;
using System.Linq;

namespace Broker.Services.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService()
        {
            _topicRepository = Container.GetInstance<ITopicRepository>();
        }

        public IEnumerable<TopicItem> GetTopicList()
        {
            var list = _topicRepository.GetAll();
            return list.Select(x => new TopicItem { Name = x.Name }).ToList();
        }
    }
}
