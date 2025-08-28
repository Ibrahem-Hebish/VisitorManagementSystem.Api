using Application.Dtos.Attachments;
using Domain.Attachments;

namespace Application.Mapping.Attachments;

public partial class AttachmentMapping : Profile
{
    public AttachmentMapping()
    {
        MapAttachmentDto();
    }
}


public partial class AttachmentMapping
{
    public void MapAttachmentDto()
    {
        CreateMap<Attachment, AttachmentDto>()
            .ForMember(dest => dest.AttachmentId, src => src.MapFrom(a => a.AttachmentId.Id.ToString()));
    }
}

