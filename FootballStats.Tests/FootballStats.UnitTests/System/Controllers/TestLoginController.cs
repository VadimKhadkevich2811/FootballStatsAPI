using FluentAssertions;
using FootballStats.API.Controllers;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Login.Dtos;
using FootballStats.UnitTests.MockData.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestLoginController
{
    private readonly Mock<IMediator> _mediatorMoq;
    public TestLoginController()
    {
        _mediatorMoq = new Mock<IMediator>();
    }

    [Fact]
    public async Task LoginAsync_ShouldReturn200Status()
    {
        ///Arrange
        Response<LoginDto> returnValue = new Response<LoginDto>(new LoginDto(), true);
        var inputData = LoginCommandMockData.GetEmptyLoginCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new LoginController(_mediatorMoq.Object);

        ///Act
        var result = await sut.LoginAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult)!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturn404Status()
    {
        ///Arrange
        Response<LoginDto> returnValue = new Response<LoginDto>(null, false);
        var inputData = LoginCommandMockData.GetNoLoginCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData!, It.IsAny<CancellationToken>()))!.ReturnsAsync(returnValue);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/login";
        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var sut = new LoginController(_mediatorMoq.Object)
        {
            ControllerContext = controllerContext
        };

        ///Act
        var result = await sut.LoginAsync(inputData!);

        ///Assert
        result.GetType().Should().Be(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)!.StatusCode.Should().Be(404);
    }
}