using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServicebusPushDataInQueueTopics.Options
{
    public class ServiceBusOption : OptionBase
    {
        public override string OptionName => "ServiceBus";

        public const string Servicebus = "ServiceBus";
        public string? Connectionstring { get; set; }
        public string? QueueName { get; set; }
    }
}
