namespace EndpointTests
{
    public class CatalogEndpointTests : IDisposable
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:5000") };


        [Fact]
        public void Test1()
        {

        }

        public void Dispose()
        {
            _httpClient.DeleteAsync("/state").GetAwaiter().GetResult();
        }
    }
}