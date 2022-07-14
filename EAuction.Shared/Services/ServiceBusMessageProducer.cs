using EAuction.Shared;
using EAuction.Shared.Enum;
using EAuction.Shared.Seller;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using System.Threading;

namespace EAuction.Shared.Services
{
    public class ServiceBusMessageProducer : IServiceBusMessageProducer
    {
        private readonly ILogger<ServiceBusMessageProducer> _logger;
        ServiceBusClient _client;
        ServiceBusSender _topicClient;

        ServiceBusProcessorOptions options = new ServiceBusProcessorOptions()
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 2
        };

        public ServiceBusMessageProducer(ILogger<ServiceBusMessageProducer>logger)
        {
            _logger = logger;
        }

        public async Task<string>SendMessageAsync(string message)
        {
            _logger.LogInformation($"message is: {message}");
            _client = new ServiceBusClient(Constants.connectionString);
            _topicClient = _client.CreateSender(Constants.topicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            await _topicClient.SendMessageAsync(serviceBusMessage);
            _logger.LogInformation($"send message to topic {Constants.topicName}");
            return $"send message to topic {Constants.topicName}";
        }

    }
}
