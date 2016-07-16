using System;
using NLog;
using ILogger = Santiago.Core.Interfaces.Logging.ILogger;

namespace Santiago.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger;

        public Logger()
        {
            _logger = LogManager.GetLogger("DefaultLogger");
        }

        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }
    }
}