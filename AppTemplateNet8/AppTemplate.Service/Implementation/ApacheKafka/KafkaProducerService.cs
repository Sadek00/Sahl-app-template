using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace AppTemplate.Service.Implementation.ApacheKafka
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync<T>(T message, string topic);
    }

    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly string _bootstrapServers;

        public KafkaProducerService(IConfiguration configuration)
        {
            _bootstrapServers = configuration["KafkaConfig:BootstrapServers"];
        }

        public async Task ProduceAsync<T>(T message, string topic)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

            var jsonMessage = SerializeMessage(message);

            var kafkaMessage = new Message<Null, string> { Value = jsonMessage };

            try
            {
                await producer.ProduceAsync(topic, kafkaMessage);
                producer.Flush(TimeSpan.FromSeconds(10)); // Ensure all messages are sent before exiting
            }
            catch (ProduceException<Null, string> ex)
            {
                // Handle the exception (log, retry, etc.)
                Console.WriteLine($"Error producing message: {ex.Error.Reason}");
                throw;
            }
        }

        private string SerializeMessage<T>(T message)
        {
            return JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}


