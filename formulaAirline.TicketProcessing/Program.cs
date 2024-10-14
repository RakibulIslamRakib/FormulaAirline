using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Xml.Schema;
using RabbitTicketingService;
using KafkaTicketingService;

Console.WriteLine("Welcome to the ticketing service");

async Task MainAsync()
{
    // Initialize RabbitMQ and Kafka Consumers
    var rabbitConsumer = new RabbitMQConsumer();
    var kafkaConsumer = new KafkaConsumer();

    // Start consuming messages from RabbitMQ and Kafka concurrently
    var rabbitTask = Task.Run(() => rabbitConsumer.ConsumeMessages());
    var kafkaTask = Task.Run(() => kafkaConsumer.ConsumeMessages());

    // Wait for both consumers to complete
    await Task.WhenAll(rabbitTask, kafkaTask);
}

// Entry point for your application
await MainAsync();
