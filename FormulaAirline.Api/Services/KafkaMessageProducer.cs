using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FormulaAirline.Api.Services
{
    public class KafkaMessageProducer : IKafkaMessageProducer
    {
        public async Task SendMessageAsync<T>(T message)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            string topic = "bookings";

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var jsonString = JsonSerializer.Serialize(message);
                    var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonString });
                    Console.WriteLine($"Delivered message to Kafka topic {deliveryResult.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    throw new Exception( e.Message );
                }
            }
        }
    }
}
