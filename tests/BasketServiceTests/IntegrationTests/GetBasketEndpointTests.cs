namespace BasketServiceTests.IntegrationTests;

public class GetBasketEndpointTests(IntegrationFixture integrationFicture) : IntegrationTest(integrationFicture)
{
    [Fact]
    public async Task GetBasket_ReturnStatusCode200_WhenBasketIsFound()
    {
        // Arrange
        var basket = new ShoppingCart(Guid.NewGuid());
        var command = new StoreBasketCommand(basket);
        var handler = Services.GetRequiredService<IRequestHandler<StoreBasketCommand, StoreBasketResult>>();
        var result = await handler.Handle(command, CancellationToken.None);
        var uri = new Uri($"https://localhost/basket/{result.UserId}");

        // Act
        var response = await Client.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
