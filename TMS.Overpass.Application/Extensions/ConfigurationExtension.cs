using Microsoft.Extensions.Configuration;

namespace TMS.Overpass.Application.Extensions;

public static class ConfigurationExtension
{
    extension(IConfiguration configuration)
    {
        public string GetRequiredConnectionString(string connectionName) => configuration.GetConnectionString(connectionName) ?? throw new InvalidOperationException($"Connection string '{connectionName}' was not found.");
        public T GetRequiredValue<T>(string key) => configuration.GetValue<T>(key) ?? throw new InvalidOperationException($"Required configuration value '{key}' is missing or null.");
    }
}