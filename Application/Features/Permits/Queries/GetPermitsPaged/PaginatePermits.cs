using Application.Dtos.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.GetPermitsPaged;

public sealed record PaginatePermits(string? Id, DateTime? CursorDate, PaginationDirection Direction) : IRequest<Response<IReadOnlyList<PermitDto>>>;
