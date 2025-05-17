using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus_nomination
{
    public class ProcessTopics
    {
        private readonly ILogger<ProcessTopics> _logger;

        public ProcessTopics(ILogger<ProcessTopics> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessTopicsForSubscription1))]
        public async Task ProcessTopicsForSubscription1(
            [ServiceBusTrigger(topicName:"%ServiceBus:TopicName%", subscriptionName:"%ServiceBus:Sub1%", Connection = "ServiceBusConnection", IsSessionsEnabled =true)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
