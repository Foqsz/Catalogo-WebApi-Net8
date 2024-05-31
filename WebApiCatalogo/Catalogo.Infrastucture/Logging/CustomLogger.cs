
namespace WebApiCatalogo.Catalogo.Infrastucture.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfiguration;

        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfiguration = config;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfiguration.LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)  
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";
            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquviLog = @"D:\ViniLogger.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquviLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
