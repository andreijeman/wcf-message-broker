using Broker.Contracts.Entities;
using Broker.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Services.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private static readonly List<Topic> _topicList = new List<Topic>() { new Topic { Name = "test" }, new Topic { Name = "test2" }, new Topic { Name = "test3" } };

        public void Add(Topic topic)
        {
            _topicList.Add(topic);
        }

        public bool ExistsByName(string name)
        {
            return _topicList.Exists(e => e.Name == name);
        }

        public IEnumerable<Topic> GetAll()
        {
            return _topicList;
        }

        public Topic GetByName(string name)
        {
            return _topicList.SingleOrDefault(e => e.Name == name);
        }
    }
}