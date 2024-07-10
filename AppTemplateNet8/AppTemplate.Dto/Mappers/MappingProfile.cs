using AutoMapper;
using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Dtos.Settings;

namespace AppTemplate.Dto.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Admin
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, UserLoginDto>().ReverseMap();
            CreateMap<ApplicationUser, UserCreateVm>().ReverseMap();
            CreateMap<ApplicationUser, UserUpdateVm>().ReverseMap();

            CreateMap<UserType, UserTypeCreateVm>().ReverseMap();
            CreateMap<UserType, UserTypeUpdateVm>().ReverseMap();

            //Settings
            CreateMap<Department, DepartmentCreateVm>().ReverseMap();
            CreateMap<Department, DepartmentUpdateVm>().ReverseMap();

            CreateMap<Designation, DesignationCreateVm>().ReverseMap();
            CreateMap<Designation, DesignationUpdateVm>().ReverseMap();

        }
    }
}
