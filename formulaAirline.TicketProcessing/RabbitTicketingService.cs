using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitTicketingService
{
    class RabbitMQConsumer
    {
        public void ConsumeMessages()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            try
            {
                using var conn = factory.CreateConnection();
                using var channel = conn.CreateModel();

                // Declare the queue to ensure it exists before consuming
                channel.QueueDeclare("bookings", durable: true, exclusive: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"RabbitMQ: New ticket processing is initiated - {message}");

                    // Here you might want to add additional processing for the message

                    // Optionally, acknowledge the message manually if autoAck is set to false
                    // channel.BasicAck(ea.DeliveryTag, false);
                };

                // Set autoAck to false if you want to manually acknowledge messages
                channel.BasicConsume(queue: "bookings", autoAck: true, consumer: consumer);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
