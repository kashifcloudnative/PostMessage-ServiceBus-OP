using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus_nomination
{
    public class ProcessQueue
    {
        private readonly ILogger<ProcessQueue> _logger;

        public ProcessQueue(ILogger<ProcessQueue> logger)
        {
            _logger = logger;
        }

        [Function(nameof(SendMessageToQueue))]
        public async Task SendMessageToQueue(
            [ServiceBusTrigger("%ServiceBus:NominationQueue%", Connection = "%ServiceBusConnection%")]
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
