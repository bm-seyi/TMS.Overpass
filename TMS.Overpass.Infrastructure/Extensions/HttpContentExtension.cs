using System.Net.Http.Json;
using System.Text.Json;

namespace TMS.Overpass.Infrastructure.Extensions;

internal static class HttpContentExtension
{
    extension(HttpContent httpContent)
    {
        public async Task<T> ReadRequiredFromJsonAsync<T>(CancellationToken cancellationToken, JsonSerializerOptions? options = null)
        {
            return await httpContent.ReadFromJsonAsync<T>(options, cancellationToken) ?? throw new InvalidOperationException($"Failed to deserialize HTTP response content into '{typeof(T).Name}'. " + "The response body was empty, invalid JSON, or did not match the expected schema.");
        }
    }
}