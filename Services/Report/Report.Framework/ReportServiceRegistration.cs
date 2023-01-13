using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Report.Framework.RabbitMQ;
using Report.Framework.Services.Abstract;
using Report.Framework.Services.Concrete;
using System.Reflection;

namespace Report.Framework
{
	public static class ReportServiceRegistration
	{
        public static IServiceCollection AddReportServices(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IContactReportService, ContactReportService>();
            services.AddScoped<IContactHttpClientService, ContactHttpClientService>();
            services.AddScoped<IExcelService, ExcelService>();

            // Rabbit MQ
            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
            services.AddScoped<RabbitMQClientService>();
            services.AddScoped<RabbitMQPublisher>();
            return services;
        }
    }
}
