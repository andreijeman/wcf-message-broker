using Broker.Contracts.Entities;
using Broker.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Services.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly List<Topic> _topicList = new List<Topic>();

        public Task Add(Topic topic)
        {
            _topicList.Add(topic);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsByName(string name)
        {
            var result = _topicList.Exists(e => e.Name == name);
            return Task.FromResult(result);
        }

        public Task<Topic> GetByName(string name)
        {
            var result = _topicList.SingleOrDefault(e => e.Name == name);
            return Task.FromResult(result);
        }
    }
}