using Application.Dtos.Permits;
using Domain.Permits.Repositories;

namespace Application.Permits.Queries.GetPermitsPaged;

public sealed record PaginatePermits(string? Id, DateTime? CursorDate, PaginationDirection Direction) : IRequest<Response<IReadOnlyList<PermitDto>>>;
