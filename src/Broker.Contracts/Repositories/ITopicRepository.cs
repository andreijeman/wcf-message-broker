using Broker.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Broker.Contracts.Repositories
{
    public interface ITopicRepository
    {
        bool ExistsByName(string name);
        Topic GetByName(string name);
        void Add(Topic topic);
        IEnumerable<Topic> GetAll();
    }
}