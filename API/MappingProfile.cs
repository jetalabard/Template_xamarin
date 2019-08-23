using AutoMapper;
using Core.Dto;
using Entities.Model;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMappingTwoDirection<User, UserDto>();
            CreateMappingTwoDirection<Role, RoleDto>();
        }

        private void CreateMappingTwoDirection<T, TW>()
        {
            CreateMap<T, TW>();
            CreateMap<TW, T>();
        }
    }
}
