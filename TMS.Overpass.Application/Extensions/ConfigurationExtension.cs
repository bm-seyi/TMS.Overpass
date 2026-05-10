using Microsoft.Extensions.Configuration;

namespace TMS.Overpass.Application.Extensions;

public static class ConfigurationExtension
{
    extension(IConfiguration configuration)
    {
        public string GetRequiredConnectionString(string connectionName) => configuration.GetConnectionString(connectionName) ?? throw new InvalidOperationException($"Connection string '{connectionName}' was not found.");
    }
}