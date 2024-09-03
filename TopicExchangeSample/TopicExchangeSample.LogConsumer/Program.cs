using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace TopicExchangeSample.LogConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("All system logs");

            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchangeName = "myTopicExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false, null);

                    string queueName = "Logs";
                    channel.QueueDeclare(queueName, true, false, false, null);
                    channel.QueueBind(queueName, exchangeName, "Log.*", null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;

                    channel.BasicConsume(queueName, true, consumer);
                    Console.WriteLine("press any key to close app");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Hello, World!");
        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            byte[] messageBytes = e.Body.ToArray();
            string message = Encoding.UTF8.GetString(messageBytes);
            Console.WriteLine($"[-] new Log : {message}");
        }
    }
}
