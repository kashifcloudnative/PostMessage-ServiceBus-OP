using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureServicebusPushDataInQueueTopics
{
    public class PushNotification
    {
        private readonly ILogger<PushNotification> _logger;
        private readonly ServiceBusClient _serviceBusClient;

        public PushNotification(ILogger<PushNotification> logger, ServiceBusClient serviceBusClient)
        {
            _logger = logger;
            _serviceBusClient = serviceBusClient;
        }

        [Function("PushMsgInQueue")]
        public async Task<IActionResult> PushMsgInQueue([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            using var requestBody = new StreamReader(req.Body);
            string reqBody = await requestBody.ReadToEndAsync();
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(reqBody);
            serviceBusMessage.ApplicationProperties["SourceSystem"] = "UDM";
            await ServiceBusUtility.SendMessageToQueueTopics(serviceBusMessage, "queuename", _serviceBusClient);
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("PushMsgInTopics")]
        public async Task<IActionResult> PushMsgInTopics([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            using var requestBody = new StreamReader(req.Body);
            string reqBody = await requestBody.ReadToEndAsync();
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(reqBody);
            serviceBusMessage.CorrelationId = Guid.NewGuid().ToString(); // Custome props
            serviceBusMessage.MessageId = Guid.NewGuid().ToString();// Custom props
            serviceBusMessage.ApplicationProperties["isNewNomination"] = true; // SQL filter
            await ServiceBusUtility.SendMessageToQueueTopics(serviceBusMessage, "topicName", _serviceBusClient);
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
