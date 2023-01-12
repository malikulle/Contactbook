using Contact.Framework.Services.Abstract;
using Contact.Framework.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Framework
{
    public static class ContactServiceRegistration
    {
        public static IServiceCollection AddContactServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IPersonService, PersonService>();
            return services;
        }
    }
}
