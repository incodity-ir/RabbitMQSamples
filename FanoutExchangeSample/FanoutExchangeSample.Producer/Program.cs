using RabbitMQ.Client;
using System.Text;

namespace FanoutExchangeSample.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost"};
            using (var connection = connectionFactory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    string exchangeName = "myFanoutExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false, null);

                    while (true)
                    {
                        Console.WriteLine("Press key : \n[1] send new message\n[2] close application");
                        if (Console.ReadLine() == "1")
                        {
                            Console.WriteLine("please write your message :");
                            string message = Console.ReadLine();
                            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish(exchangeName, "", null, messageBytes);
                            Console.WriteLine("your message has been successfully sent to all groups!");
                            Console.WriteLine("press any key to countinue");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else break;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
