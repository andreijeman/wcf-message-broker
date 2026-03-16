using Broker.Contracts.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace Broker.Contracts.Services
{
    [ServiceContract]
    public interface ITopicService
    {
        [OperationContract]
        IEnumerable<TopicItem> GetTopicList();
    }
}
