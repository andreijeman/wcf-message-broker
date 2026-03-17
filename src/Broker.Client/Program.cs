using Broker.Contracts.Faults;
using System;
using System.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;
using Message = Broker.Contracts.Data.Message;

namespace Broker.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("  Run Subscriber:  1");
            Console.WriteLine("  Run Publisher:   2");
            Console.Write("> ");

            var opt = Console.ReadLine();

            if (opt == "1")
            {
                RunSubscriber();
            }
            else if (opt == "2")
            {
                RunPublisher();
            }
        }

        static void RunPublisher()
        {
            var client = new PublisherServiceClient();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Topic: ");
                var topic = Console.ReadLine();

                Console.Write("Message: ");
                var text = Console.ReadLine();

                try
                {
                    client.Publish(new Message { Topic = topic, Text = text });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        static void RunSubscriber()
        {
            var subscriberClient = new SubscriberServiceClient();
            var topicClient = new TopicServiceClient();

            while (true)
            {
                Console.WriteLine("Options: ");
                Console.WriteLine("  Subscribe:     1");
                Console.WriteLine("  Show topics:   2");
                Console.Write("> ");

                var opt = Console.ReadLine();

                if(opt == "1")
                {
                    Console.WriteLine();
                    Console.Write("Topic: ");
                    var topic = Console.ReadLine();
                
                    try
                    {
                        var response = subscriberClient.Subscribe(topic);
                  
                        Task.Run(() => HandleSubscriberMessageQueue(response.QueuePath));
                    }
                    catch (FaultException<SubscriptionFault> e)
                    {
                        Console.WriteLine(e.Detail.Description);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if(opt == "2")
                {
                    var topicList = topicClient.GetTopicList();

                    Console.WriteLine();
                    Console.Write("Topics: ");
                    foreach (var topic in topicList)
                    {
                        Console.Write(topic.Name + "; ");
                    }
                    Console.WriteLine();
                }
                else Console.WriteLine("Unknown option");

                Console.WriteLine();
            }
        }

        static void HandleSubscriberMessageQueue(string queuePath)
        {
            try
            {
                var queue = new MessageQueue(queuePath);
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Message) });

                queue.ReceiveCompleted += OnSubscriberMessageQueueReceived;
                queue.BeginReceive();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void OnSubscriberMessageQueueReceived(Object source, ReceiveCompletedEventArgs eventArgs)
        {
            MessageQueue queue = (MessageQueue)source;

            var message = queue.EndReceive(eventArgs.AsyncResult);

            var body = (Message)message.Body;
            Console.WriteLine();
            Console.WriteLine($"{body.Topic}: {body.Text}");

            queue.BeginReceive();
        }
    }
}
