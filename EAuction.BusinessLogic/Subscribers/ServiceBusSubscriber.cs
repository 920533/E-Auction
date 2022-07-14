using Azure.Messaging.ServiceBus;
using EAuction.Shared.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.BusinessLogic.MessageBroker
{
    public class ServiceBusSubscriber : IHostedService
    {
        private readonly ILogger<ServiceBusSubscriber> _logger;
        private readonly IServiceBusMessageProducer _serviceBusMessageSender;
        ServiceBusClient client;
        ServiceBusProcessor processor;

        ServiceBusProcessorOptions options = new ServiceBusProcessorOptions()
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 2
        };
        public ServiceBusSubscriber(ILogger<ServiceBusSubscriber> logger, IServiceBusMessageProducer serviceBusMessageSender)
        {
            _logger = logger;
            _serviceBusMessageSender = serviceBusMessageSender;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            client = new ServiceBusClient(EAuction.Shared.Constants.connectionString);
            processor = client.CreateProcessor(EAuction.Shared.Constants.topicName, EAuction.Shared.Constants.subscriberName,options);
            try
            {
                // add handler to process message 
                processor.ProcessMessageAsync += MessageHandler;

                //add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                //start processing 
                await processor.StartProcessingAsync();
            }
           finally
            {

              
            }
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if(processor != null)
            {
                await processor.DisposeAsync();
            }
            if(client != null)
            {
                await client.DisposeAsync();
            }
        }


        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: '{body}' from subscription: '{EAuction.Shared.Constants.subscriberName}'");
            Thread.Sleep(new TimeSpan(0,1,0));//hh/mm/ss

            //complete the message. message is deleted from the subscription
            await args.CompleteMessageAsync(args.Message,args.CancellationToken);

        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
