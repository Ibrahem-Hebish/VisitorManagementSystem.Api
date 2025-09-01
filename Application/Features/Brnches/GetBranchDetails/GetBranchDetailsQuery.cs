using Application.Dtos.Branch;

namespace Application.Features.Brnches.GetBranchDetails;

public record GetBranchDetailsQuery(string BranchId) : IRequest<Response<BranchDetails>> { }
