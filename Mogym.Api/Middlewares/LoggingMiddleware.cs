using Mogym.Application.Interfaces.ILog;

namespace Mogym.Api.Middlewares
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
            var permalink = context.Request.Path.Value;
            var ip = context.Connection.RemoteIpAddress.ToString();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userLoggingService = scope.ServiceProvider.GetRequiredService<IUserLoggingService>();
                await _userLoggingService.Save(permalink, ip);

            }



            await _next(context);
        }
    }
}
