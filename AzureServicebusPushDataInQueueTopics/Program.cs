using AzureServiceBus_nomination;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
            //.ConfigureFunctionsWorkerDefaults()
           .ConfigureServices(services =>
           {
               services.UseLocalSetting();
           });


// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

host.Build().Run();
