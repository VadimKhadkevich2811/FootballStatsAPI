using FluentAssertions;
using FootballStats.Application.Controllers;
using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.SignUp.Commands;
using FootballStats.UnitTests.MockData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FootballStats.UnitTests.System.Controllers;

public class TestSignUpController
{
    private readonly Mock<IMediator> _mediatorMoq;
    public TestSignUpController()
    {
        _mediatorMoq = new Mock<IMediator>();
    }

    [Fact]
    public async Task SignUpAsync_ShouldReturn201Status()
    {
        ///Arrange
        SignUpDTO returnValue = new SignUpDTO();
        var inputData = SignUpCommandMockData.GetEmptySignUpCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new SignUpController(_mediatorMoq.Object);

        ///Act
        var result = await sut.SignUpAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(CreatedResult));
        (result as CreatedResult).StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task SignUpAsync_ShouldReturn400Status()
    {
        ///Arrange
        SignUpDTO returnValue = null;
        var inputData = SignUpCommandMockData.GetNoSignUpCommandData();
        _mediatorMoq.Setup(x => x.Send(inputData, It.IsAny<CancellationToken>())).ReturnsAsync(returnValue);
        var sut = new SignUpController(_mediatorMoq.Object);
        
        ///Act
        var result = await sut.SignUpAsync(inputData);

        ///Assert
        result.GetType().Should().Be(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }
}