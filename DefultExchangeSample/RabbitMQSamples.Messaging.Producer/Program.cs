using RabbitMQ.Client;
using System.Text;

namespace RabbitMQSamples.Messaging.Producer
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
                    channel.QueueDeclare(queueName,false,false,false,null);
                    Console.WriteLine($"queue {queueName} was created successed!");
                    Console.WriteLine("\nPlease write your message and Press [ENTER] : \n");
                    string message = Console.ReadLine();
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message).ToArray();
                    channel.BasicPublish("", queueName, null,messageBytes);
                    Console.WriteLine("your message sent successed!");
                    Console.ReadLine();
                }
            }
            
        }
    }
}
