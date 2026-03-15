using Broker.Contracts.Entities;
using System.Threading.Tasks;

namespace Broker.Contracts.Repositories
{
    public interface ITopicRepository
    {
        Task<bool> ExistsByName(string name);
        Task<Topic> GetByName(string name);
        Task Add(Topic topic);
    }
}