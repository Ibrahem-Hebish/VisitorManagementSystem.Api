namespace Application.Permits.Commands.ExtendPermit;
public sealed class ExtendPermitCommandHandler(
    IPermitQueryRepository permitQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<ExtendPermitCommand, Response<string>>
{
    public async Task<Response<string>> Handle(ExtendPermitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userid = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var permit = await permitQueryRepository.GetByIdAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

            if (permit is null)
                return NotFouned<string>();

            var managerId = new UserId(new Guid(userid!));

            permit.ExtendEndDate(request.NewEndDate);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Trigger background job to mark permit as expired when the time = end time

            return Success("Permit was approved successfully.");
        }
        catch (Exception ex)
        {
            // logs

            return InternalServerError<string>();

        }

    }
}