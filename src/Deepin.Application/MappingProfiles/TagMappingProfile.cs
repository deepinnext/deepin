using System;
using AutoMapper;
using Deepin.Application.Queries;
using Deepin.Domain.Entities;

namespace Deepin.Application.MappingProfiles;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<Tag, TagDto>();
    }
}
