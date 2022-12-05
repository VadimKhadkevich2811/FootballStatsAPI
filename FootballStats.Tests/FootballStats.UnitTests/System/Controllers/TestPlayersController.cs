using FluentAssertions;
using FootballStats.Application.Controllers;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayers;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using FootballStats.UnitTests.MockData.Players;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;

namespace FootballStats.UnitTests.System.Controllers;

public class TestPlayersController
{
    private readonly Mock<IMediator> _mediatorMoq;
    private readonly Mock<IUriService> _uriServiceMoq;
    public TestPlayersController()
    {
        _mediatorMoq = new Mock<IMediator>();
        _uriServiceMoq = new Mock<IUriService>();
    }

    [Fact]
    public async Task GetAllPlayersAsync_ShouldReturn200Status()
    {
        ///Arrange
        PlayersListWithCountDTO returnValue = new PlayersListWithCountDTO(new List<PlayerReadDTO>(), 0);
        var filter = new PlayersQueryStringParams();
        var inputData = new GetAllPlayersQuery(filter);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetAllPlayersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.GetAllPlayersAsync(filter);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
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
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetPlayerByIdAsync_ShouldReturn404Status()
    {
        ///Arrange
        int queryParam = 1;
        PlayerReadDTO? returnValue = null;
        var inputData = new GetPlayerByIdQuery(queryParam);
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetPlayerByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)!.StatusCode.Should().Be(404);
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
        (result as CreatedAtRouteResult)!.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task CreatePlayerAsync_ShouldReturn400Status()
    {
        ///Arrange
        var inputData = CreatePlayerCommandMockData.GetNoCreatePlayerCommandData();
        PlayerReadDTO? returnValue = null;
        _mediatorMoq.Setup(x => x.Send(inputData!, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreatePlayerAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
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
        (result as NoContentResult)!.StatusCode.Should().Be(204);
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
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
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
        (result as NoContentResult)!.StatusCode.Should().Be(204);
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
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
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
        (result as NoContentResult)!.StatusCode.Should().Be(204);
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
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetFreePlayersByDateAsync_ShouldReturn200Status()
    {
        ///Arrange
        PlayersListWithCountDTO returnValue = new PlayersListWithCountDTO(new List<PlayerReadDTO>(), 0);
        var testDate = DateTime.Now;
        var inputData = new GetFreePlayersQuery(testDate);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetFreePlayersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/free";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.GetFreePlayersByDateAsync(testDate);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }
}