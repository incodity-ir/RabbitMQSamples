using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DirectExchangeSample.MailConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to mail box");

            var connectionFactory = new ConnectionFactory();
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchangeName = "myDirectExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);

                    string queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, exchangeName, "MailBox", null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;

                    channel.BasicConsume(queueName,true,consumer);

                    Console.WriteLine("press any key to close app");
                    Console.ReadKey();

                }
            }

        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var messageBytes = e.Body.ToArray();
            string message = Encoding.UTF8.GetString(messageBytes);
            Console.WriteLine($"[-] Received email : {message}");
        }
    }
}
