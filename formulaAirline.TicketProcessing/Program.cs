
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Xml.Schema;

Console.WriteLine("wellcome to ticketing service");

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
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, EventArgs) =>
    {
        var body = EventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"New ticket processing is initiated  - {message}");
    };

    channel.BasicConsume("lookings", true, consumer);
    Console.ReadKey();

    
}
catch (Exception ex)
{
    throw new Exception(ex.Message);
}
