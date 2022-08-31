using Microsoft.Extensions.Azure;
using MoviePortal.Common.AzureStorageServices.Interfaces;
using MoviePortal.Common.AzureStorageServices.Services;
using MoviePortal.RequestConsumerService;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((hostContext, services) =>
	{
		IConfiguration configuration = hostContext.Configuration;
		services.AddHostedService<RequestHandler>();
		services.AddHttpClient<RequestHandler>(client =>
		{
			client.BaseAddress = new Uri(configuration.GetSection("RapidOptions:RapidBaseUrl").Value);
		});
		
		services.AddAzureClients(clientsBuilder =>
		{
			clientsBuilder.AddServiceBusClient(configuration.GetSection("ServiceBusOptions:ServiceBusConnectionString"))
			  .WithName("ClientService")
			  .ConfigureOptions(options =>
			  {
				  options.RetryOptions.Delay = TimeSpan.FromMilliseconds(50);
				  options.RetryOptions.MaxDelay = TimeSpan.FromSeconds(5);
				  options.RetryOptions.MaxRetries = 3;
			  });
		});
		services.AddSingleton<ITableStorageService, TableStorageService>();
	})
	.Build();

await host.RunAsync();
