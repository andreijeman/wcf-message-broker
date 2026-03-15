using Broker.Contracts.data;
using Broker.Contracts.Faults;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Contracts
{
    [ServiceContract]
    public interface IPublisher
    {
        [OperationContract]
        [FaultContract(typeof(PublicationFault))]
        Task Publish(Message message);
    }
}