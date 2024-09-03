using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FanoutExchangeSample.FrontendConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to FrontEnd Developers messaging box");

            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchangeName = "myFanoutExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false, null);

                    string queueName = "FrontendDevelopers";
                    channel.QueueDeclare(queueName,true,false,false,null);
                    channel.QueueBind(queueName,exchangeName,"",null);

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
            Console.WriteLine($"[-] received new message : {message}");
        }
    }
}
