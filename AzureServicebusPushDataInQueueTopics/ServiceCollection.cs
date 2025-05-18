using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using AzureServicebusPushDataInQueueTopics.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AzureServiceBus_nomination
{
    public static class ServiceCollection
    {
        public static void UseLocalSetting(this IServiceCollection services)
        {
            // setting the service bus client
            services.SetServiceBusClient();
        }

        // How to convert it to generic 
        public static void OptionConfigurationServiceBus(this IServiceCollection services)
        {
            services.AddOptions<ServiceBusOption>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(ServiceBusOption.Servicebus).Bind(settings);
                });
        }

        public static void OptionConfiguration<TOptionBase>(this IServiceCollection services) where TOptionBase : OptionBase
        {
            services.AddOptions<TOptionBase>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(settings.OptionName).Bind(settings);
                }).ValidateDataAnnotations().ValidateOnStart();
        }


        public static void SetServiceBusClient(this IServiceCollection services)
        {
            // setting the service bus client
            //services.OptionConfigurationServiceBus();
            // use generic method
            services.OptionConfiguration<ServiceBusOption>();
            services.AddSingleton(serviceProvider =>
            {
                var optionSource = serviceProvider.GetRequiredService<IOptions<ServiceBusOption>>(); // Read config
                var options = optionSource.Value;
                var connectionString = options.Connectionstring; // Fetch from options
                var serviceBusClient = new ServiceBusClient(connectionString, new ServiceBusClientOptions()
                {
                    RetryOptions = new ServiceBusRetryOptions
                    {
                        Mode = ServiceBusRetryMode.Exponential,
                        MaxRetries = 3,
                        Delay = TimeSpan.FromSeconds(1),
                        MaxDelay = TimeSpan.FromSeconds(10)

                    }
                });
                return serviceBusClient;
            });
        }
    }
}
