using RabbitMQ.Client;
using System.Text;

namespace DirectExchangeSample.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory() { HostName="localhost"};
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchangeName = "myDirectExchange";
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);

                    
                    while (true)
                    {
                        Console.WriteLine("Press [1] to send message and press \n[2] to close application");
                        if (Console.ReadLine() == "1")
                        {
                            Console.WriteLine("Please Enter your message :");
                            string message = Console.ReadLine().ToString();

                            Console.WriteLine("Specify your sending message method : \n [1] send by email \n [2] send by sms \n Please send number");
                            var clientMethod = Console.ReadLine();

                            string RoutKey = "Drafts";

                            if (int.TryParse(clientMethod, out int method))
                            {
                                switch (method)
                                {
                                    case 1:
                                        RoutKey = "MailBox";
                                        Console.WriteLine("Please enter your email adddress : ");
                                        string email = Console.ReadLine();
                                        message = $"From : {email} \n Replay to : {email} \n Message : {message}  \n Regards";
                                        break;
                                    case 2:
                                        RoutKey = "MessageBox";
                                        Console.WriteLine("Please enter your phone number :");
                                        string phone = Console.ReadLine();
                                        message = $"Phone number : {phone} \n Message : {message}";
                                        break;
                                }
                            }

                            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchangeName, RoutKey, null, messageBytes);
                            Console.WriteLine("your message was sent successed! \n");
                            Console.Clear();
                        }
                        else break;
                    }
                    Console.ReadKey();
                }

            }
        }
    }
}
