using FluentAssertions;
using FootballStats.Application.Controllers;
using FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;
using FootballStats.ApplicationModule.Coaches.Queries.GetAllCoaches;
using FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;
using FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.UnitTests.MockData.Coaches;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestCoachesController
{
    private readonly Mock<IMediator> _mediatorMoq;
    private readonly Mock<IUriService> _uriServiceMoq;
    public TestCoachesController()
    {
        _mediatorMoq = new Mock<IMediator>();
        _uriServiceMoq = new Mock<IUriService>();
    }

    [Fact]
    public async Task GetAllCoachesAsync_ShouldReturn200Status()
    {
        ///Arrange
        Response<CoachesListWithCountDto> returnValue = new Response<CoachesListWithCountDto>(new CoachesListWithCountDto(new List<CoachReadDto>(), 0), true);
        var filter = new CoachesQueryStringParams();
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetAllCoachesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.GetAllCoachesAsync(filter);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetCoachByIdAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<CoachReadDto> returnValue = new Response<CoachReadDto>(new CoachReadDto(), true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetCoachByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetCoachByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetCoachByIdAsync_ShouldReturn404Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<CoachReadDto> returnValue = new Response<CoachReadDto>(null, true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetCoachByIdQuery>(), It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetCoachByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreateCoachAsync_ShouldReturn200Status()
    {
        ///Arrange
        var inputData = CreateCoachCommandMockData.GetEmptyCreateCoachCommandData();
        Response<CoachReadDto> returnValue = new Response<CoachReadDto>(new CoachReadDto(), true);
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreateCoachAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(CreatedAtRouteResult));
        (result as CreatedAtRouteResult)!.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task CreateCoachAsync_ShouldReturn400Status()
    {
        ///Arrange
        var inputData = CreateCoachCommandMockData.GetNoCreateCoachCommandData();
        Response<CoachReadDto> returnValue = new Response<CoachReadDto>(null, false);
        _mediatorMoq.Setup(x => x.Send(inputData!, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreateCoachAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeleteCoachAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteCoachCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeleteCoachAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteCoachAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteCoachCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeleteCoachAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdateCoachAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        var inputData = UpdateCoachCommandMockData.GetEmptyUpdateCoachCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdateCoachAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        var inputData = UpdateCoachCommandMockData.GetEmptyUpdateCoachCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdateCoachDetailAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        var inputData = UpdateCoachDetailCommandMockData.GetEmptyUpdateCoachDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdateCoachDetailAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        var inputData = UpdateCoachDetailCommandMockData.GetEmptyUpdateCoachDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetFreeCoachesByDateAsync_ShouldReturn200Status()
    {
        ///Arrange
        Response<CoachesListWithCountDto> returnValue = new Response<CoachesListWithCountDto>(new CoachesListWithCountDto(new List<CoachReadDto>(), 0), true);
        var testDate = DateTime.Now;
        var inputData = new GetFreeCoachesQuery(testDate);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetFreeCoachesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/free";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.GetFreeCoachesByDateAsync(testDate);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }
}