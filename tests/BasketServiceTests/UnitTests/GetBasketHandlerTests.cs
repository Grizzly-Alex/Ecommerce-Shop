namespace BasketServiceTests.UnitTests;

public class GetBasketHandlerTests
{
    private readonly Mock<IBasketRepository> _repositoryMock;

    public GetBasketHandlerTests()
    {
        _repositoryMock = new Mock<IBasketRepository>();
    }


    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenBasketIsFound()
    {
        //Arrange
        var command = new GetBasketQuery(Guid.NewGuid());

        _repositoryMock.Setup(x => x.GetBasket(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ShoppingCart());

        var handler = new GetBasketHandler(_repositoryMock.Object);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Cart.Should().NotBeNull();   
    }


    [Fact]
    public async Task Handle_Should_ThrowException_WhenBasketIsNotFound()
    {
        // Arrange
        var query = new GetBasketQuery(Guid.NewGuid());
        ShoppingCart result = null;

        _repositoryMock.Setup(x => x.GetBasket(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        var handler = new GetBasketHandler(_repositoryMock.Object);

        //Act
        Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<BasketNotFoundException>(act);
        Assert.Equal($"Entity Basket with UserId {query.UserId} was not found.", exception.Message);
    }
}
