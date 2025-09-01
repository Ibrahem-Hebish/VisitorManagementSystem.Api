using Application.Features.Visitors.CreateNewVisitor;
using Application.Services.UnitOfWork;
using Domain.TenantDomain.Users.Enums;
using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.Repositories;
using Moq;
using System.Net;

namespace Application.Test.Permits;

public class CreateVisitorCommandHandlerTests
{
    private readonly Mock<IVisitorQueryRepository> _visitorQueryRepositoryMock;
    private readonly Mock<IVisitorCommandRepository> _visitorCommandRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateVisitorCommandHandler _handler;

    public CreateVisitorCommandHandlerTests()
    {
        _visitorQueryRepositoryMock = new Mock<IVisitorQueryRepository>();
        _visitorCommandRepositoryMock = new Mock<IVisitorCommandRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateVisitorCommandHandler(
            _visitorQueryRepositoryMock.Object,
            _visitorCommandRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    private static CreateVisitorCommand BuildCommand() =>
        new CreateVisitorCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "123456789",
            NationalId = "987654321",
            Gender = PersonGender.Male
        };

    [Fact]
    public async Task Handler_Should_Return_BadRequest_When_Visitor_Already_Exists()
    {
        var command = BuildCommand();
        var existingVisitor = Visitor.Create("John", "Doe", "john@example.com", "123456789", "987654321", PersonGender.Male);

        _visitorQueryRepositoryMock
            .Setup(x => x.GetByEmailAsync(command.Email))
            .ReturnsAsync(existingVisitor);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Handler_Should_Create_Visitor_When_Not_Exists()
    {
        var command = BuildCommand();

        _visitorQueryRepositoryMock
            .Setup(x => x.GetByEmailAsync(command.Email))
            .ReturnsAsync((Visitor?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        _visitorCommandRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Visitor>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.True(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Handler_Should_Return_InternalServerError_When_Exception_Occurs()
    {
        var command = BuildCommand();

        _visitorCommandRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Visitor>()))
            .ThrowsAsync(new Exception());

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}

