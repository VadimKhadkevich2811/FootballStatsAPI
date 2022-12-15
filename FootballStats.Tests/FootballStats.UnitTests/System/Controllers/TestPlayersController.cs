using FluentAssertions;
using FootballStats.API.Controllers;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Commands.DeletePlayer;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Players.Queries.GetAllPlayers;
using FootballStats.Application.Players.Queries.GetFreePlayers;
using FootballStats.Application.Players.Queries.GetPlayerById;
using FootballStats.UnitTests.MockData.Players;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestPlayersController
{
    private readonly Mock<IMediator> _mediatorMoq;
    private readonly Mock<IUriService> _uriServiceMoq;
    private readonly Mock<IResponseWrapper<PlayerReadDto, PlayersListWithCountDto>> _responseWrapperMoq;
    public TestPlayersController()
    {
        _mediatorMoq = new Mock<IMediator>();
        _uriServiceMoq = new Mock<IUriService>();
        _responseWrapperMoq = new Mock<IResponseWrapper<PlayerReadDto, PlayersListWithCountDto>>();
    }

    [Fact]
    public async Task GetAllPlayersAsync_ShouldReturn200Status()
    {
        ///Arrange
        Response<PlayersListWithCountDto> returnValue = new Response<PlayersListWithCountDto>(new PlayersListWithCountDto(new List<PlayerReadDto>(), 0));
        var filter = new PlayersQueryStringParams();
        var inputData = new GetAllPlayersQuery(filter);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetAllPlayersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
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
        Response<PlayerReadDto> returnValue = new Response<PlayerReadDto>(new PlayerReadDto(), true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetPlayerByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        Response<PlayerReadDto> returnValue = new Response<PlayerReadDto>(null, true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetPlayerByIdQuery>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        Response<PlayerReadDto> returnValue = new Response<PlayerReadDto>(new PlayerReadDto(), true);
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

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
        Response<PlayerReadDto> returnValue = new Response<PlayerReadDto>(null, false);
        _mediatorMoq.Setup(x => x.Send(inputData!, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.CreatePlayerAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeletePlayerAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeletePlayerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

        ///Act
        var result = await sut.DeletePlayerAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeletePlayerAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeletePlayerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.DeletePlayerAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdatePlayerAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        var inputData = UpdatePlayerCommandMockData.GetEmptyUpdatePlayerCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

        ///Act
        var result = await sut.UpdatePlayerAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdatePlayerAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        var inputData = UpdatePlayerCommandMockData.GetEmptyUpdatePlayerCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.UpdatePlayerAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdatePlayerDetailAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        var inputData = UpdatePlayerDetailCommandMockData.GetEmptyUpdatePlayerDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

        ///Act
        var result = await sut.UpdatePlayerDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdatePlayerDetailAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        var inputData = UpdatePlayerDetailCommandMockData.GetEmptyUpdatePlayerDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/players";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        Response<PlayersListWithCountDto> returnValue = new Response<PlayersListWithCountDto>(new PlayersListWithCountDto(new List<PlayerReadDto>(), 0));
        var testDate = DateTime.Now;
        var inputData = new GetFreePlayersQuery(testDate);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetFreePlayersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/free";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new PlayersController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
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