using Microsoft.Extensions.Configuration;

namespace Logging.Consumer;

public class DataBaseConfiguration
{
    private readonly IConfigurationRoot _configuration;

    public DataBaseConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        _configuration = builder.Build();
    }

    public string GetRabbitMqUrl()
    {
        return _configuration["RabbitMqUrl"];
    }

    public string GetUrlGateway()
    {
        return _configuration["UrlGateway"];
    }
}