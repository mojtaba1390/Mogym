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
using Mogym.Application.AutoMapper;
using Mogym.Application.AutoMapper.User;
using Mogym.Application.Interfaces.ICache;
using Mogym.Application.Services.Cache;

namespace Mogym.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(LoginRecord_User));


            #region fluent validation
            services.AddControllers()
                .AddFluentValidation(v =>
                {
                    v.ImplicitlyValidateChildProperties = true;
                    v.ImplicitlyValidateRootCollectionElements = true;
                    v.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(LoginValidate)));
                });

            #endregion



            #region Service Life Time
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISeriLogService, SerilogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<IPermissionService, PermissionService>();
            #endregion

            return services;
        }
    }

}
