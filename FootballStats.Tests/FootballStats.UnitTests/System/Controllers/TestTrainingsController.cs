using FluentAssertions;
using FootballStats.Application.Controllers;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainings;
using FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;
using FootballStats.UnitTests.MockData.Trainings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestTrainingsController
{
    private readonly Mock<IMediator> _mediatorMoq;
    private readonly Mock<IUriService> _uriServiceMoq;
    public TestTrainingsController()
    {
        _mediatorMoq = new Mock<IMediator>();
        _uriServiceMoq = new Mock<IUriService>();
    }

    [Fact]
    public async Task GetAllTrainingsAsync_ShouldReturn200Status()
    {
        ///Arrange
        Response<TrainingsListWithCountDto> returnValue = new Response<TrainingsListWithCountDto>(new TrainingsListWithCountDto(new List<TrainingReadDto>(), 10));
        var filter = new TrainingsQueryStringParams();
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetAllTrainingsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/trainings";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.GetAllTrainingsAsync(filter);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetTrainingByIdAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<TrainingReadDto> returnValue = new Response<TrainingReadDto>(new TrainingReadDto(), true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetTrainingByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetTrainingByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetTrainingByIdAsync_ShouldReturn404Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<TrainingReadDto> returnValue = new Response<TrainingReadDto>(null, true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<GetTrainingByIdQuery>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.GetTrainingByIdAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreateTrainingAsync_ShouldReturn200Status()
    {
        ///Arrange
        var inputData = CreateTrainingCommandMockData.GetEmptyCreateTrainingCommandData();
        Response<TrainingReadDto> returnValue = new Response<TrainingReadDto>(new TrainingReadDto(), true);
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreateTrainingAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(CreatedAtRouteResult));
        (result as CreatedAtRouteResult)!.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task CreateTrainingAsync_ShouldReturn400Status()
    {
        ///Arrange
        var inputData = CreateTrainingCommandMockData.GetNoCreateTrainingCommandData();
        Response<TrainingReadDto> returnValue = new Response<TrainingReadDto>(null, false);
        _mediatorMoq.Setup(x => x.Send(inputData!, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.CreateTrainingAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeleteTrainingAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteTrainingCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeleteTrainingAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteTrainingAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteTrainingCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.DeleteTrainingAsync(queryParam);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdateTrainingAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        var inputData = UpdateTrainingCommandMockData.GetEmptyUpdateTrainingCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateTrainingAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdateTrainingAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        var inputData = UpdateTrainingCommandMockData.GetEmptyUpdateTrainingCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateTrainingAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdateTrainingDetailAsync_ShouldReturn200Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(true, true);
        var inputData = UpdateTrainingDetailCommandMockData.GetEmptyUpdateTrainingDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateTrainingDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdateTrainingDetailAsync_ShouldReturn400Status()
    {
        ///Arrange
        int queryParam = 1;
        Response<bool> returnValue = new Response<bool>(false, false);
        var inputData = UpdateTrainingDetailCommandMockData.GetEmptyUpdateTrainingDetailCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new TrainingsController(_mediatorMoq.Object, _uriServiceMoq.Object);

        ///Act
        var result = await sut.UpdateTrainingDetailAsync(queryParam, inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)!.StatusCode.Should().Be(400);
    }
}