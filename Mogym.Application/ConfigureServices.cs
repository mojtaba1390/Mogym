using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;

namespace Mogym.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(v =>
                {
                    v.ImplicitlyValidateChildProperties = true;
                    v.ImplicitlyValidateRootCollectionElements = true;
                    v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
            return services;
        }
    }

}
