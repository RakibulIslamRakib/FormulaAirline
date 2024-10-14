using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;

namespace FormulaAirline.Api.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
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

                // Declare the queue, ensure it's not exclusive and doesn't auto-delete
                channel.QueueDeclare("lookings", durable: true, exclusive: false);

                var jsonString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonString);

                // Set basic properties, like making the message persistent
                //var properties = channel.CreateBasicProperties();
               // properties.Persistent = true;

                // Publish the message to the queue
                channel.BasicPublish( "",  "lookings", body: body);
            }
            catch (BrokerUnreachableException ex)
            {
                throw new Exception(ex.Message);
            }           
        }
    }
}
