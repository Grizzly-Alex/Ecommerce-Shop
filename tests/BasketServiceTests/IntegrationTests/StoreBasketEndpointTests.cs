namespace BasketServiceTests.IntegrationTests;

public class StoreBasketEndpointTests(IntegrationFixture integrationFixture) : IntegrationTest(integrationFixture)
{
    private readonly Uri uri = new("https://localhost/basket");

    [Fact]
    public async Task StoreBasket_ReturnStatusCode201_WhenCreatedSuccess()
    {
        // Arrange
        var cart = new ShoppingCart() 
        {           
            UserId = Guid.NewGuid(),           
            Items = [
                new ShoppingCartItem() 
                { 
                    Quantity = 2, 
                    Price = 100, 
                    ProductName = "LTD M300FM",
                    ProductId = Guid.NewGuid(),
                }],    
        };
        var request = new StoreBasketRequest(cart);
        string test = JsonConvert.SerializeObject(request);
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        //Act
        var response = await Client.PostAsync(uri, content, CancellationToken.None);

        //Assert

        //for test
        var strBodyContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(strBodyContent)!;
        //

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
