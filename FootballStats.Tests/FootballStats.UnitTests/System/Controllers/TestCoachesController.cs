using FluentAssertions;
using FootballStats.Application.Controllers;
using FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;
using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllCoachesQuery;
using FootballStats.ApplicationModule.Trainings.Queries.GetCoachById;
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
        CoachesListWithCountDTO returnValue = new CoachesListWithCountDTO(new List<CoachReadDTO>(), 0);
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
        CoachReadDTO returnValue = new CoachReadDTO();
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
        CoachReadDTO? returnValue = null;
        var inputData = new GetCoachByIdQuery(queryParam);
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetCoachByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NotFoundResult));
        (result as NotFoundResult)!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreateCoachAsync_ShouldReturn200Status()
    {
        ///Arrange
        var inputData = CreateCoachCommandMockData.GetEmptyCreateCoachCommandData();
        CoachReadDTO returnValue = new CoachReadDTO();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreateCoachAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(CreatedAtRouteResult));
        (result as CreatedAtRouteResult)!.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task CreateCoachAsync_ShouldReturn400Status()
    {
        ///Arrange
        var inputData = CreateCoachCommandMockData.GetNoCreateCoachCommandData();
        CoachReadDTO? returnValue = null;
        _mediatorMoq.Setup(x => x.Send(inputData!, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreateCoachAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeleteCoachAsync_ShouldReturn204Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = true;
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteCoachCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeleteCoachAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        (result as NoContentResult)!.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task DeleteCoachAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = false;
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteCoachCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeleteCoachAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdateCoachAsync_ShouldReturn204Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = true;
        var inputData = UpdateCoachCommandMockData.GetEmptyUpdateCoachCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        (result as NoContentResult)!.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task UpdateCoachAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = false;
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
    public async Task UpdateCoachDetailAsync_ShouldReturn204Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = true;
        var inputData = UpdateCoachDetailCommandMockData.GetEmptyUpdateCoachDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        (result as NoContentResult)!.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task UpdateCoachDetailAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        bool returnValue = false;
        var inputData = UpdateCoachDetailCommandMockData.GetEmptyUpdateCoachDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new CoachesController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateCoachDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }
}