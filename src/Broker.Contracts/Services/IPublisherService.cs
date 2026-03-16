using Broker.Contracts.Data;
using Broker.Contracts.Faults;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Contracts.Services
{
    [ServiceContract]
    public interface IPublisherService
    {
        [OperationContract]
        [FaultContract(typeof(PublicationFault))]
        void Publish(Message message);
    }
}