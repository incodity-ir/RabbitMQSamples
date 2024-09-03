using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DirectExchangeSample.SmsConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to message box");

            var connectionFactory = new ConnectionFactory() { HostName = "localhost"};
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchangeName = "myDirectExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);

                    string queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, exchangeName, "MessageBox", null);

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
            var messageBytes = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(messageBytes);
            Console.WriteLine($"[-] new sms : \n {message}");
        }
    }
}
