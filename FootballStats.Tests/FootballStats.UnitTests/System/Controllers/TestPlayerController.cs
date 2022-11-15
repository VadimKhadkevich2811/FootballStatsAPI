using FluentAssertions;
using FootballStats.Application.Controllers;
using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using FootballStats.ApplicationModule.SignUp.Commands;
using FootballStats.UnitTests.MockData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestPlayerController
{
    private readonly Mock<IMediator> _mediatorMoq;
    private readonly Mock<IUriService> _uriServiceMoq;
    public TestPlayerController()
    {
        _mediatorMoq = new Mock<IMediator>();
        _uriServiceMoq = new Mock<IUriService>();
    }

    [Fact]
    public async Task GetAllPlayersAsync_ShouldReturn200Status()
    {
        ///Arrange
        List<PlayerReadDTO> returnValue = new List<PlayerReadDTO>();
        //var inputData = new GetAllPlayersQuery();
        //_mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        //var result = await sut.GetAllPlayersAsync();

        ///Assert
        //result.GetType().Should().Be(typeof(OkObjectResult));
        //(result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPlayerByIdAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        PlayerReadDTO returnValue = new PlayerReadDTO();
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetPlayerByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetPlayerByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPlayerByIdAsync_ShouldReturn404Status()
    {
        ///Arrange
        int queryParam = 1;
        PlayerReadDTO returnValue = null;
        var inputData = new GetPlayerByIdQuery(queryParam);
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetPlayerByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NotFoundResult));
        (result as NotFoundResult).StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreatePlayerAsync_ShouldReturn200Status()
    {
        ///Arrange
        var inputData = CreatePlayerCommandMockData.GetEmptyCreatePlayerCommandData();
        PlayerReadDTO returnValue = new PlayerReadDTO();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreatePlayerAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(CreatedAtRouteResult));
        (result as CreatedAtRouteResult).StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task CreatePlayerAsync_ShouldReturn400Status()
    {
        ///Arrange
        var inputData = CreatePlayerCommandMockData.GetNoCreatePlayerCommandData();
        PlayerReadDTO returnValue = null;
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreatePlayerAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeletePlayerAsync_ShouldReturn204Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = true;
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeletePlayerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeletePlayerAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        (result as NoContentResult).StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task DeletePlayerAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = false;        
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeletePlayerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeletePlayerAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdatePlayerAsync_ShouldReturn204Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = true;
        var inputData = UpdatePlayerCommandMockData.GetEmptyUpdatePlayerCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdatePlayerAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        (result as NoContentResult).StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task UpdatePlayerAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = false;
        var inputData = UpdatePlayerCommandMockData.GetEmptyUpdatePlayerCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdatePlayerAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdatePlayerDetailAsync_ShouldReturn204Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = true;
        var inputData = UpdatePlayerDetailCommandMockData.GetEmptyUpdatePlayerDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdatePlayerDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        (result as NoContentResult).StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task UpdatePlayerDetailAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = false;
        var inputData = UpdatePlayerDetailCommandMockData.GetEmptyUpdatePlayerDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdatePlayerDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }
}