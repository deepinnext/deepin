using AutoMapper;
using Deepin.Application.Queries;
using Deepin.Domain.PageAggregates;

namespace Deepin.Application.MappingProfiles;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<Note, NoteDto>(MemberList.Destination)
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
    }
}
