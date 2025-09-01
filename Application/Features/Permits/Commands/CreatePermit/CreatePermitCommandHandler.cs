using Application.Services.File;
using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.Repositories;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.Repositories;

namespace Application.Features.Permits.Commands.CreatePermit;

public sealed class CreatePermitCommandHandler(
    IPermitCommandRepository permitCommandRepository,
    IFileService fileService,
    IVisitorQueryRepository visitorQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<CreatePermitCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreatePermitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userid = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var requesterId = new UserId(new Guid(userid!));

            var buildingId = new BuildingId(new Guid(request.BuildingId));

            var emails = request.Visitors.Select(v => v.Email).ToList();

            var exsisitingVisitors = await visitorQueryRepository.GetExsistingVisitors(emails, cancellationToken);

            var exsisitingVisitorsEmails = exsisitingVisitors.Select(v => v.Email).ToList();

            var newVisitors = request.Visitors.Where(
                v => !exsisitingVisitorsEmails.Contains(v.Email)).ToList();

            var PermitVisitors = new List<Visitor>(exsisitingVisitors);

            var newVisitorEntities = newVisitors.Select(v =>
                                                   Visitor.Create(v.FirstName, v.LastName, v.Email, v.PhoneNumber,
                                                    v.NationalId, v.Gender)).ToList();

            PermitVisitors.AddRange(newVisitorEntities);

            var permit = Permit.Create(request.StartDate, request.EndDate, request.Reason, buildingId,
                                       request.FloorNumber, requesterId, PermitVisitors);

            if (request.Attachments?.Count > 0)
            {
                foreach (var attachmentdto in request.Attachments)
                {
                    var filePath = await fileService.UploadAsync(attachmentdto.File, "Permits");

                    var attachment = Attachment.Create(attachmentdto.Type, filePath, permit.PermitId);

                    permit.AddAttachment(attachment);
                }
            }

            if (request.BelongingDtos?.Count > 0)
            {
                foreach (var belongingDto in request.BelongingDtos)
                {
                    if (!string.IsNullOrEmpty(belongingDto.PlateNumber) && !string.IsNullOrEmpty(belongingDto.Color))
                    {
                        var car = new Car(belongingDto.Name, belongingDto.Description,
                                          permit.PermitId, belongingDto.PlateNumber, belongingDto.Color);

                        permit.AddBelonging(car);
                    }
                    else
                    {
                        var belonging = new Belonging(belongingDto.Name, belongingDto.Description,
                                                       permit.PermitId);

                        permit.AddBelonging(belonging);
                    }
                }
            }

            await permitCommandRepository.CreateAsync(permit, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Trigger background job to mark permit as expired when the time = end time

            return Created<string>();
        }
        catch (Exception ex)
        {
            // Logging

            return InternalServerError<string>();
        }

    }
}
