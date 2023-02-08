namespace ApiCatalogo.Logging
{
    public class CustomeLoggerProviderConfiguration
    {
        public LogLevel logLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}
