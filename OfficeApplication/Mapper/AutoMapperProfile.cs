using AutoMapper;
using OfficeApplication.Shared;
using System.Diagnostics.CodeAnalysis;

namespace OfficeApplication.Api.Mapper
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, Employee_DTO>().ReverseMap();
        }
    }
}
