using MessageBroker.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageBroker
{
    public static class Receive
    {
        public static void ReceiveMessage(IConnectionFactory factory)
        {
            User user = new User();
            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "QueueRenan",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var arrMessage = Encoding.UTF8.GetString(body).Split('|');
                        user.Name = arrMessage[0];
                        user.LastName = arrMessage[1];
                        user.Phone = arrMessage[2];
                        Console.WriteLine("First Name: " + user.Name );
                        Console.WriteLine("Last Name: " + user.LastName);
                        Console.WriteLine("Phone Number: " + user.Phone);
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");


                    };
                    channel.BasicConsume(
                        queue: "QueueRenan",
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine("Press Enter To Exit");
                    Console.ReadKey();
                }
            }
        }
    }
}
