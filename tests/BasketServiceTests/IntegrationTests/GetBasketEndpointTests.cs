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


    [Fact]
    public async Task GetBasket_ReturnStatusCode404_WhenBasketIsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var uri = new Uri($"https://localhost/basket/{userId}");

        // Act
        var response = await Client.GetAsync(uri, CancellationToken.None);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();

        var strBodyContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(strBodyContent)!;

        problemDetails.Status.Should().Be(StatusCodes.Status404NotFound);
    }


    [Fact]
    public async Task GetBasket_ReturnStatusCode400_WhenProvideIncorrectData()
    {
        // Arrange
        var nonvalidId = Guid.NewGuid().ToString().Replace('-', '?');
        var uri = new Uri($"https://localhost/basket/{nonvalidId}");

        // Act
        var response = await Client.GetAsync(uri, CancellationToken.None);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();

        var strBodyContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(strBodyContent)!;

        problemDetails.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
}
