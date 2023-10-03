using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Services;
using Mogym.Application.Services.Log;
using Mogym.Infrastructure;
using Mogym.Application.Validation.User;

namespace Mogym.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            #region fluent validation
            services.AddControllers()
                .AddFluentValidation(v =>
                {
                    v.ImplicitlyValidateChildProperties = true;
                    v.ImplicitlyValidateRootCollectionElements = true;
                    v.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(RegisterUserValidate)));
                });

            #endregion



            #region Service Life Time
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISeriLogService, SerilogService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            return services;
        }
    }

}
