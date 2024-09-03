using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics.Tracing;
using System.Text;

namespace RabbitMQSamples.Messaging.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queueName = "MessagingQueue";
                    channel.QueueDeclare(queueName, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += Consumer_Received;

                    channel.BasicConsume(queueName, true, consumer);
                    Console.WriteLine("Press [ENTER] to close program \n");
                    Console.ReadLine();
                }
            }
            
        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"[ - ] Message recevid : {message}");
        }
    }
}
