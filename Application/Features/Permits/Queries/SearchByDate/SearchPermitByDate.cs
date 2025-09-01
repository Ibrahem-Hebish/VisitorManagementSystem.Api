using Application.Dtos.Permits;

namespace Application.Features.Permits.Queries.SearchByDate;

public sealed record SearchPermitByDateQuery(DateTime StartDate, DateTime EndDate) : IRequest<Response<List<PermitDto>>>;
