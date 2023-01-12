using Microsoft.Extensions.DependencyInjection;
using Report.Framework.Services.Abstract;
using Report.Framework.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework
{
	public static class ReportServiceRegistration
	{
        public static IServiceCollection AddReportServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IContactReportService, ContactReportService>();
            services.AddScoped<IContactHttpClientService, ContactHttpClientService>();
            services.AddScoped<IExcelService, ExcelService>();
            return services;
        }
    }
}
