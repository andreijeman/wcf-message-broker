using Broker.Services.Repositories;
using Broker.Services.Services;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Broker.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => RunPublisherService());
            Task.Run(() => RunSubscriberService());
            Task.Run(() => RunTopicService());

            Console.ReadKey();
        }

        static void RunPublisherService()
        {
            ServiceHost selfHost = new ServiceHost(typeof(PublisherService));

            try
            {
                selfHost.Open();
                Console.WriteLine("The publisher service is ready.");
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }

        static void RunSubscriberService()
        {
            ServiceHost selfHost = new ServiceHost(typeof(SubscriberService));

            try
            {
                selfHost.Open();
                Console.WriteLine("The subscriber service is ready.");
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }

        static void RunTopicService()
        {
            ServiceHost selfHost = new ServiceHost(typeof(TopicService));

            try
            {
                selfHost.Open();
                Console.WriteLine("The topic service is ready.");
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
