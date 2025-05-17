using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([ServiceBusTrigger("nomination-queue", Connection = "test-connectionstring")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
