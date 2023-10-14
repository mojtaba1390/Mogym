using Mogym.Infrastructure.Interfaces.Log;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Infrastructure.Repositories.Log
{
    public class SeriLogRepository: ISeriLogRepository
    {

        private readonly ILogger _logger;

        public SeriLogRepository()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: "Server=45.139.10.231;Database=MogymLog;User Id=MogymUser;Password=Mojt@b@12345;TrustServerCertificate=true",
                    tableName: "SeriLog",
                    autoCreateSqlTable: false)
                .CreateLogger();
        }






        public void LogInformation(string message)
        {
            _logger.Information(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }
    }
}
