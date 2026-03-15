using Broker.Contracts.data;
using Broker.Contracts.Data;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Contracts
{
    [ServiceContract]
    public interface ISubscriber
    {
        [OperationContract]
        [FaultContract(typeof(SubscriptionFault))]
        Task<SubscriptionResponse> Subscribe(string topicName);
    }
}