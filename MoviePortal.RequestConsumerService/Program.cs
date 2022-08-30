using Microsoft.Extensions.Azure;
using MoviePortal.RequestConsumerService;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddHostedService<RequestHandler>();
		services.AddHttpClient<RequestHandler>(client =>
		{
			client.BaseAddress = new Uri("https://movies-app1.p.rapidapi.com/api/movies");
		});

		services.AddAzureClients(clientsBuilder =>
		{
			clientsBuilder.AddServiceBusClient("**")
			  .WithName("ClientService")
			  .ConfigureOptions(options =>
			  {
				  options.RetryOptions.Delay = TimeSpan.FromMilliseconds(50);
				  options.RetryOptions.MaxDelay = TimeSpan.FromSeconds(5);
				  options.RetryOptions.MaxRetries = 3;
			  });
		});
	})
	.Build();

await host.RunAsync();
