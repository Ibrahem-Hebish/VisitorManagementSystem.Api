using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.EntryLogs.Repositories;
using Serilog;

namespace Application.Features.EntryLogs.CreateEntryLog;


public sealed class CreateEntryLogCommandHandler(
    IEntryLogCommandRepository entryLogCommandRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<CreateEntryLogCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateEntryLogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var entryLog = Entrylog.Create(new UserId(new Guid(userId!)), new PermitId(new Guid(request.PermitId)));

            await entryLogCommandRepository.AddAsync(entryLog);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Created<string>("EntryLog created successfully.");

        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);

            return InternalServerError<string>();
        }
    }
}
