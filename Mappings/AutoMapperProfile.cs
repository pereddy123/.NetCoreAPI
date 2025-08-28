using AutoMapper;
using WorkSphereAPI.DTOs;
using WorkSphereAPI.Models;

namespace WorkSphereAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<TaskItem, TaskDto>()
           .ForMember(dest => dest.AssignedToUsername, opt => opt.MapFrom(src => src.AssignedToUser.Username));

            CreateMap<TaskDto, TaskItem>();
        }
    }
}
