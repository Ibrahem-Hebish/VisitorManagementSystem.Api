using Application.Dtos.Branch;

namespace Application.Admin.GetBranchDetails;

public record GetBranchDetailsQuery(string BranchId) : IRequest<Response<BranchDetails>> { }
