using Confluent.Kafka;
using System;
using System.Threading;

namespace KafkaTicketingService
{
	class KafkaConsumer
	{
		public void ConsumeMessages()
		{
			var config = new ConsumerConfig
			{
				GroupId = "ticketing-consumer-group",
				BootstrapServers = "localhost:9092",
				AutoOffsetReset = AutoOffsetReset.Earliest
			};

			using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
			{
				consumer.Subscribe("bookings");

				try
				{
					while (true)
					{
						var consumeResult = consumer.Consume(CancellationToken.None);
						var message = consumeResult.Message.Value;
						Console.WriteLine($"Kafka: New ticket processing is initiated - {message}");
					}
				}
				catch (ConsumeException e)
				{
					Console.WriteLine($"Error occurred: {e.Error.Reason}");
				}
				finally
				{
					consumer.Close();
				}
			}
		}
	}
}
