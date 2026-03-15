using Broker.Contracts.Data;
using Broker.Contracts.Faults;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Contracts.Services
{
    [ServiceContract]
    public interface ISubscriber
    {
        [OperationContract]
        [FaultContract(typeof(SubscriptionFault))]
        Task<SubscriptionResponse> Subscribe(string topicName);
    }
}