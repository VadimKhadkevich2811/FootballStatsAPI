using FluentAssertions;
using FootballStats.API.Controllers;
using FootballStats.Application.Coaches.Commands.DeleteCoach;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Coaches.Queries.GetAllCoaches;
using FootballStats.Application.Coaches.Queries.GetCoachById;
using FootballStats.Application.Coaches.Queries.GetFreeCoaches;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using FootballStats.UnitTests.MockData.Coaches;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestCoachesController
{
    private readonly Mock<IMediator> _mediatorMoq;
    private readonly Mock<IUriService> _uriServiceMoq;
    private readonly Mock<IResponseWrapper<CoachReadDto, CoachesListWithCountDto>> _responseWrapperMoq;
    public TestCoachesController()
    {
        _mediatorMoq = new Mock<IMediator>();
        _uriServiceMoq = new Mock<IUriService>();
        _responseWrapperMoq = new Mock<IResponseWrapper<CoachReadDto, CoachesListWithCountDto>>();
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

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
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
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

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
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

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
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

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
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object);

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
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/coaches";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
        {
            ControllerContext = controllerContext
        };

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
        httpContext.Request.Path = "/api/coaches/free";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object, _responseWrapperMoq.Object)
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