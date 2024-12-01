using AutoMapper;
using Deepin.Application.Queries;
using Deepin.Domain.Entities;

namespace Deepin.Application.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>(MemberList.Destination);
    }
}
