using Microsoft.AspNetCore.Http.Extensions;
using Mogym.Application.Interfaces.ILog;

namespace Mogym.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public LoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();

            var permalink = context.Request.Path.Value;
            if (permalink == "/")
               await InsertLog(permalink, ip);


            var referer = context.Request.Headers["Referer"];

            if (!string.IsNullOrWhiteSpace(referer))
                await InsertLog(referer.ToString(), ip);


            await _next(context);
        }


        private async Task InsertLog(string permalink,string ip)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userLoggingService = scope.ServiceProvider.GetRequiredService<IUserLoggingService>();
                await _userLoggingService.Save(permalink, ip);

            }
        }

    }
}
