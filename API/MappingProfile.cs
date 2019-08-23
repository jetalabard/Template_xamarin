using AutoMapper;
using Entities.Model;
using Core.Dto;

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

        private void CreateMappingTwoDirection<T, W>()
        {
            CreateMap<T, W>();
            CreateMap<W, T>();
        }
    }
}
