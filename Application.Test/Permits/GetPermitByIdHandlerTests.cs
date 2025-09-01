using Application.Features.Permits.Queries.GetPermitById;
using Application.Mapping.Permits;
using Application.Mapping.Visitors;
using AutoMapper;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Permits.Repositories;
using Domain.TenantDomain.Users.Enums;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Visitors;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Test.Permits;

public class GetPermitByIdHandlerTests
{
    private Mock<IPermitQueryRepository> _repository;
    private IMapper _mapper;
    private GetPermitByIdHandler _handler;

    public GetPermitByIdHandlerTests()
    {
        _repository = new Mock<IPermitQueryRepository>();

        var config = new MapperConfiguration(x =>
        {
            x.AddProfile<PermitMapping>();
            x.AddProfile(new VisitorMapping());

        }, new LoggerFactory());

        _mapper = config.CreateMapper();

        _handler = new GetPermitByIdHandler(_repository.Object, _mapper);
    }

    [Fact]
    public async Task Handler_Should_Return_NotFound_When_The_Permit_Does_not_Exsist()
    {
        var permitId = Guid.NewGuid().ToString();

        var query = new GetPermitByIdQuery(permitId);

        Permit permit = null!;

        _repository.Setup(x => x.GetByIdAsync(new PermitId(new Guid(permitId)), CancellationToken.None))
                         .ReturnsAsync(permit);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.False(result.IsSuccess);

    }

    [Fact]
    public async Task Handler_Should_Return_Success_When_The_Permit_Exsists()
    {
        var visitor = Visitor.Create("ibrahem", "ahmed", "hema@gmail.com", "0122415278", "30103843838495", PersonGender.Male);

        List<Visitor> visitors = [
            visitor
            ];

        Permit permit = Permit.Create(
            DateTime.UtcNow, DateTime.UtcNow.AddMinutes(50),
            "Test reason", new BuildingId(Guid.NewGuid()), 3,
            new UserId(Guid.NewGuid()), visitors);

        var query = new GetPermitByIdQuery(permit.PermitId.Value.ToString());



        _repository.Setup(x => x.GetByIdAsync(permit.PermitId, CancellationToken.None))
                         .ReturnsAsync(permit);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);

        Assert.Equal(result?.Data?.Visitors.Count, 1);

    }
}



