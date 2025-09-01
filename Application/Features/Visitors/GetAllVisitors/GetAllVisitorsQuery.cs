using Application.Dtos.Visitors;

namespace Application.Features.Visitors.GetAllVisitors;

public sealed record GetAllVisitorsQuery : IRequest<Response<List<VisitorDto>>>;
