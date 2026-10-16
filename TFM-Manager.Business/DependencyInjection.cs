using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Business.Services;

namespace TFM_Manager.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IcompanyService, CompanyService>();
            return services;
        }
    }
}
