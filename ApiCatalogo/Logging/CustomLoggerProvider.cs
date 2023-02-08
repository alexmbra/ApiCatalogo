using System.Collections.Concurrent;

namespace ApiCatalogo.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomeLoggerProviderConfiguration loggerConfig;

        private readonly ConcurrentDictionary<string, CustomerLogger> loggers = new();

        public CustomLoggerProvider(CustomeLoggerProviderConfiguration loggerConfig)
        {
            this.loggerConfig = loggerConfig;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
