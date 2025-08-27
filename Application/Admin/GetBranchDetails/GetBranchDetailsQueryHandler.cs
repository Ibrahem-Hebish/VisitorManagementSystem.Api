using Application.Dtos.Branch;
using Domain.Branches.ObjectValues;
using Domain.Branches.Repositories;

namespace Application.Admin.GetBranchDetails;

public class GetBranchDetailsQueryHandler(
    IBranchQueryRepository branchQueryRepository,
    IMapper mapper)
    : ResponseHandler,
    IRequestHandler<GetBranchDetailsQuery, Response<BranchDetails>>
{
    public async Task<Response<BranchDetails>> Handle(GetBranchDetailsQuery request, CancellationToken cancellationToken)
    {
        var branch = await branchQueryRepository.GetDetailsAsync(new BranchId(new Guid(request.BranchId)), cancellationToken);

        if (branch is null)
            return NotFouned<BranchDetails>("There is no branch with this id");

        var branchDto = mapper.Map<BranchDetails>(branch);

        return Success(branchDto);
    }
}
