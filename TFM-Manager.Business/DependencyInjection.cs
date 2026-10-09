using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register your business services here
            // Example: services.AddScoped<IMyService, MyService>();
            return services;
        }
    }
}
