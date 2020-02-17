
using RabbitMQ.Client;
using System;

namespace MessageBroker
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                string conditional;
                string conditionalLower;
                var factory = GetConnectionFactory();



                //PUBLISH
                do
                {
                    Console.WriteLine("Do you want to Publish messages?");
                    conditional = Console.ReadLine();
                    conditionalLower = conditional?.ToLower();
                    if ((conditionalLower == "y") || (conditionalLower == "n"))
                    {
                        break;
                    }
                } while (true);
                if (conditionalLower=="y")
                {
                    Send.PrepareToSendMessage(factory);
                }


                //CONSUME
                do
                {
                    Console.WriteLine("Do you want to consume messages?");
                    conditional = Console.ReadLine();
                    conditionalLower = conditional?.ToLower();
                    if ((conditionalLower == "y") || (conditionalLower == "n"))
                    {
                        break;
                    }
                } while (true);

                if (conditionalLower == "y")
                {
                    Receive.ReceiveMessage(factory);
                }

                Console.WriteLine("Press Enter To Exit");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static IConnectionFactory GetConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            return connectionFactory;
        }


    }
}
