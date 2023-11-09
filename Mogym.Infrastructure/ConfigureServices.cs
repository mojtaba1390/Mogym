using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Infrastructure.Interfaces.ILog;
using Mogym.Infrastructure.Interfaces.Log;
using Mogym.Infrastructure.Repositories.Log;

namespace Mogym.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {


            services.AddLogging();


            services.AddTransient<IUserLoggingRepository, UserLoggingRepository>();
            services.AddTransient<ISeriLogRepository, SeriLogRepository>();



            return services;
        }
    }
}
