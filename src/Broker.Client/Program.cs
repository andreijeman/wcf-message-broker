using Broker.Client.ServiceReference1;
using Broker.Client.ServiceReference2;
using System;
using System.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;
using Message = Broker.Client.ServiceReference2.Message;

namespace Broker.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1 to run Publisher or 2 to run Subscriber");
            var option = Console.ReadLine();

            if (option == "1")
            {
                RunPublisher();
            }
            else if (option == "2")
            {
                RunSubscriber();
            }
            else Console.WriteLine("Unknown option");
        }

        static void RunPublisher()
        {
            var client = new PublisherClient();

            while (true)
            {
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
            var client = new SubscriberClient();

            while (true)
            {
                Console.Write("Topic: ");
                var topic = Console.ReadLine();

                var response = client.Subscribe(topic);

                Task.Run(() =>
                {
                    try
                    {
                        var queue = new MessageQueue(response.QueuePath);
                        var message = queue.Receive(new TimeSpan(0, 0, 3));

                        if (message != null)
                        {
                            Console.WriteLine(message.Body);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"In Topic <{response.TopicName}> occurred a error: {e}");
                    }

                });

            }
        }
    }
}
