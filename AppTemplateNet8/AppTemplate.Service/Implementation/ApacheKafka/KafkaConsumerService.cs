using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AppTemplate.Service.Implementation.ApacheKafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ConsumerConfig _config;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly List<string> _topics = new List<string>
        {
            // All topics list...
            KafkaTopicManager.CREATE_REASON_PRODUCE_TOPIC,
            KafkaTopicManager.UPDATE_REASON_PRODUCE_TOPIC,
            KafkaTopicManager.MODIFY_REASON_PRODUCE_TOPIC
    };

        public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cancellationTokenSource = new CancellationTokenSource();
            _config = new ConsumerConfig
            {
                BootstrapServers = _configuration["KafkaConfig:BootstrapServers"],
                GroupId = _configuration["KafkaConfig:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
            this.scopeFactory = scopeFactory;
        }

        #region Get All Available Topics

        private List<string> GetAllAvailableTopics()
        {
            List<string> topicNames = new();
            using (var adminClient = new AdminClientBuilder(_config).Build())
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                topicNames = metadata.Topics.Select(a => a.Topic).ToList();
            }
            return topicNames;
        }

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            //consumer.Subscribe(_topics);
            //All available live topics
            var allAvailableTopics = GetAllAvailableTopics();
            //Inventory topics available in live
            var availableCrmTopics = _topics.FindAll(topic => allAvailableTopics.Contains(topic));

            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();

            if (availableCrmTopics.Count == 0)
                return;

            //Subscribing inventory live topics
            consumer.Subscribe(availableCrmTopics);
            try
            {

                while (!stoppingToken.IsCancellationRequested)
                {

                    var consumeResult = await Task.Run(() => consumer.Consume(stoppingToken), stoppingToken);

                    if (consumeResult.Message != null)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        var isConsumed = await ProcessMessageAsync(consumeResult);
                        if (isConsumed)
                        {
                            consumer.Commit(consumeResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}. Stack Trace: {ex.StackTrace}");
                // Handle the exception or log it appropriately
            }
            //finally
            //{
            //    consumer.Close();
            //}

        }

        private async Task<bool> ProcessMessageAsync(ConsumeResult<Ignore, string> message)
        {
            try
            {
                var topic = message.Topic;

                using (var scope = scopeFactory.CreateScope())
                {
                    //var kafkaProcessConsumerService = scope.ServiceProvider.GetRequiredService<KafkaProcessConsumerService>();
                    //var _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                    //var reasonService = scope.ServiceProvider.GetRequiredService<IReasonService>();

                    //switch (topic)
                    //{
                    //    case KafkaTopicManager.CREATE_REASON_PRODUCE_TOPIC:                         
                    //        return (await reasonService.Create(JsonConvert.DeserializeObject<Reason>(message.Value))).IsSuccess;

                    //    case KafkaTopicManager.UPDATE_REASON_PRODUCE_TOPIC:
                    //        return (await reasonService.Update(JsonConvert.DeserializeObject<Reason>(message.Value))).IsSuccess;

                    //    case KafkaTopicManager.MODIFY_REASON_PRODUCE_TOPIC:
                    //        var freeShippingObject = JsonConvert.DeserializeObject<Reason>(message.Value);
                    //        return (await reasonService.ModifyStatus(freeShippingObject.Id, freeShippingObject.StatusId, freeShippingObject.UpdatedBy)).IsSuccess;     

                    //    default:
                    //        _logger.LogWarning($"Received message from unknown topic: {topic}");
                    //        return false;
                    //}
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}. Stack Trace: {ex.StackTrace}");
                return false;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // Call StopKafkaConsumer to gracefully stop the Kafka consumer
            await StopKafkaConsumer();

            // Perform any additional cleanup here

            // Call the base StopAsync method
            await base.StopAsync(cancellationToken);
        }

        public async Task StopKafkaConsumer()
        {
            _cancellationTokenSource.Cancel();
            await Task.Delay(TimeSpan.FromSeconds(10));
            _cancellationTokenSource.Dispose();
        }

    }
}
