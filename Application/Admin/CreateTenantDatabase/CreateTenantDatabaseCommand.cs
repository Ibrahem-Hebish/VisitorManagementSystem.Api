using Application.CustomResponse;
using MediatR;

namespace Application.Admin.CreateTenantDatabase;

public record CreateTenantDatabaseCommand : IRequest<Response<string>>;
