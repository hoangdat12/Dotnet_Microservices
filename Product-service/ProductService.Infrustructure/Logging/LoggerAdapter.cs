using Microsoft.Extensions.Logging;
using ProductService.Application.Contract.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrustructure.Logging
{
    public class LoggerAdapter<T>(ILoggerFactory loggerFactory) : IAppLogger<T>
    {
        private readonly ILogger<T> _logger = loggerFactory.CreateLogger<T>();
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
