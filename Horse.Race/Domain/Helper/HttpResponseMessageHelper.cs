using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Helper;

public static class HttpResponseMessageHelper
{
    private static readonly JsonSerializerOptions? Options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    static HttpResponseMessageHelper() => HttpResponseMessageHelper.Options.Converters.Add((System.Text.Json.Serialization.JsonConverter) new JsonStringEnumConverter());

    public static async Task<T?> ReadContentAsAsync<T>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default (CancellationToken),
        JsonSerializerOptions? options = null)
    {
        if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
            return default (T);
        if (typeof (T) == typeof (string))
        {
            string contentString = await response.Content.ReadAsStringAsync(cancellationToken);
            return (T) Convert.ChangeType((object) contentString, typeof (T));
        }
        string json = await response.Content.ReadAsStringAsync(cancellationToken);
        T? deserialized = JsonSerializer.Deserialize<T>(json, options ?? HttpResponseMessageHelper.Options);
        return deserialized;
    }
}