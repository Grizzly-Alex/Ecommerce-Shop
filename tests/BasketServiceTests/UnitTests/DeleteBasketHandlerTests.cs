namespace BasketServiceTests.UnitTests;

public class DeleteBasketHandlerTests
{
    private readonly Mock<IBasketRepository> _repositoryMock;

    public DeleteBasketHandlerTests()
    {
        _repositoryMock = new Mock<IBasketRepository>();       
    }


    [Fact]  
    public async Task Handle_Should_ReturnSuccessResult_WhenBasketSuccessfullyDeleted()
    {
        //Arrange
        var command = new DeleteBasketCommand(Guid.NewGuid());

        _repositoryMock.Setup(x => x.DeleteBasket(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new DeleteBasketHandler(_repositoryMock.Object);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }


    [Fact]
    public async Task Handle_Should_ThrowException_WhenBasketNotFound()
    {
        //Arrange
        var command = new DeleteBasketCommand(Guid.NewGuid());

        _repositoryMock.Setup(x => x.DeleteBasket(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new DeleteBasketHandler(_repositoryMock.Object);

        //Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<BasketNotFoundException>(act);
        Assert.Equal($"Entity Basket with UserId {command.UserId} was not found.", exception.Message);
    }
}
