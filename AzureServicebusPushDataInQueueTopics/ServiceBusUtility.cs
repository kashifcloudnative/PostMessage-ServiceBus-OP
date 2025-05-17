using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServicebusPushDataInQueueTopics
{
    public static class ServiceBusUtility
    {
        public static async Task SendMessageToQueueTopics(ServiceBusMessage serviceBusMessage, string queueTopicName, ServiceBusClient serviceBusClient)
        {
            ServiceBusSender? serviceBusSender = null;
            try
            {
                serviceBusSender = serviceBusClient.CreateSender(queueTopicName);
                await serviceBusSender.SendMessageAsync(serviceBusMessage);
            }
            finally
            {
                if (serviceBusSender != null)
                {
                    await serviceBusSender.DisposeAsync();
                }
            }
        }
    }
}

