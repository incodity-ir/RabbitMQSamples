using RabbitMQ.Client;
using System.Text;

namespace TopicExchangeSample.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchangeName = "myTopicExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false, null);
                    string routKey = "Log.Any";
                    while (true)
                    {
                        Console.WriteLine("Press key : \n[1] submit new log\n[2] close application");
                        if (Console.ReadLine() == "1")
                        {
                            Console.WriteLine("please write event log :");
                            string message = Console.ReadLine();
                            if (message.Contains("error"))
                            {
                                routKey = "Log.Error";
                            }
                            else if (message.Contains("info"))
                            {
                                routKey = "Log.Info";
                            }
        
                            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish(exchangeName, routKey, null, messageBytes);
                            Console.WriteLine("your log has been successfully submit!");
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
