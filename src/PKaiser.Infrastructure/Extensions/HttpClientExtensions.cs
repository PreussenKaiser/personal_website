using System.Net.Http.Json;

namespace PKaiser.Infrastructure.Extensions;

/// <summary>
/// Contains extension methods for <see cref="HttpClient"/>.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="client"></param>
    /// <param name="requestUri"></param>
    /// <param name="value"></param>
    /// <returns></returns>
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
