namespace EndpointTests;

public static class TestHelpers
{
    private const string _jsonMediaType = "application/json";
    private const int _expectedMaxElapsedMilliseconds = 1000;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };


    /// <summary>
    /// Checks the content type is JSON, deserialize it into the supplied type parameter, then calls into the second method for the other assertions.
    /// </summary>
    public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
    HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
    T expectedContent)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
            await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
    }

    /// <summary>
    /// Checks the status code is what is expected and also checks the performance assertions.
    /// </summary>
    private static void AssertCommonResponseParts(Stopwatch stopwatch,
        HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
    }

    /// <summary>
    /// Helps to serialize the JSON that we want to send to the API.
    /// </summary>
    public static StringContent GetJsonStringContent<T>(T model)
        => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);
}
