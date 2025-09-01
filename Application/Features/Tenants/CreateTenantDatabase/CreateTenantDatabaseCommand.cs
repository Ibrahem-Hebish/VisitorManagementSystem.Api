using Application.CustomResponse;
using MediatR;

namespace Application.Features.Tenants.CreateTenantDatabase;

public record CreateTenantDatabaseCommand : IRequest<Response<string>>;
