using System;
using System.Text;
using MessageBroker.Model;
using RabbitMQ.Client;


namespace MessageBroker
{
    public static class Send
    {
        
        public static void PrepareToSendMessage(IConnectionFactory factory)
        {
            User user = new User();


            for (int i = 0; i < 2; i++)
            {
                user = UserData();
                SendMessage(user, factory);
                Console.WriteLine("");
                Console.WriteLine("=============================================");
                string conditional;
                string conditionalLower;

                do
                {
                    Console.WriteLine("Do you want register another user? Yes (y) or No (n)");
                    conditional = Console.ReadLine();
                    conditionalLower = conditional?.ToLower();
                    if ((conditionalLower == "y") || (conditionalLower == "n"))
                    {
                        break;
                    }
                } while (true);
                if (conditionalLower == "n")
                {
                    i = 5;
                }
                else
                {
                    
                    i = 0;
                }
            }

        }


        public static void SendMessage(User user, IConnectionFactory factory)
        {
            
            using(var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "QueueRenan",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null); 
                    var body = Encoding.ASCII.GetBytes(user.Name+"|"+ user.LastName + "|"+user.Phone);
                    channel.BasicPublish(
                        exchange: "", 
                        routingKey:"QueueRenan", 
                        basicProperties: null,
                        body: body);
                    Console.WriteLine("Message sent");

                }
            }
        
        }

        public static User UserData()
        {
            User user = new User();
            Console.WriteLine("Enter First Name: ");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Enter phone Number: ");
            user.Phone = Console.ReadLine();
            return user;
        }
    }
}
