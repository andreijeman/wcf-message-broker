using Broker.Contracts.Data;
using Broker.Contracts.Faults;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Contracts.Services
{
    [ServiceContract]
    public interface ISubscriberService
    {
        [OperationContract]
        [FaultContract(typeof(SubscriptionFault))]
        SubscriptionResponse Subscribe(string topicName);
    }
}