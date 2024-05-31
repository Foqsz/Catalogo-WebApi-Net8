namespace WebApiCatalogo.Catalogo.Infrastucture.Logging
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning; //define o nivel minimo de log a ser registrado, com o padrao Loglevel.Warning
        public int EventoId { get; set; } = 0; // define o id de log, com padrao sendo zero
    }
}
