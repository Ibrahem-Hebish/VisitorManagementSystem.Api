namespace Application.Permits.Commands.ApprovePermit;

public sealed class ApprovePermitCommandHandler(
    IPermitQueryRepository permitQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<ApprovePermitCommand, Response<string>>
{
    public async Task<Response<string>> Handle(ApprovePermitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userid = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var permit = await permitQueryRepository.GetByIdAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

            if (permit is null)
                return NotFouned<string>();

            var managerId = new UserId(new Guid(userid!));

            permit.Approve(managerId);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Success("Permit was approved successfully.");
        }
        catch (Exception ex)
        {
            // logs

            return InternalServerError<string>();

        }

    }
}
