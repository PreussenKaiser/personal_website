using System.Net.Http.Json;

namespace PKaiser.Infrastructure.Extensions;

/// <summary>
/// Contains extension methods for <see cref="HttpClient"/>.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Deletes a JSON object from a document.
    /// </summary>
    /// <typeparam name="T">The type to delete.</typeparam>
    /// <param name="client">The client to send the request with.</param>
    /// <param name="requestUri">The URI of the JSON.</param>
    /// <param name="value">The value to delete.</param>
    /// <returns>The status of the request.</returns>
    public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Content = JsonContent.Create(value),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(requestUri, UriKind.Relative)
        };

        return await client.SendAsync(request);
    }
}
