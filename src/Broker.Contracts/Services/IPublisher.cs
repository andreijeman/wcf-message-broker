using Broker.Contracts.Data;
using Broker.Contracts.Faults;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Contracts.Services
{
    [ServiceContract]
    public interface IPublisher
    {
        [OperationContract]
        [FaultContract(typeof(PublicationFault))]
        Task Publish(Message message);
    }
}