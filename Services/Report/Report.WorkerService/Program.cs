using CommonProject.Http;
using RabbitMQ.Client;
using Report.WorkerService;
using Report.WorkerService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {
        IConfiguration Configuration = hostContext.Configuration;
        var aa = Configuration.GetConnectionString("RabbitMQ");
        services.Configure<APISettings>(Configuration.GetSection("APISettings"));
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton<ReportHttpClientService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
