using Core.Extensions;

namespace Core.Dto
{
    public class RoleDto : Dto
    {
        public string Label { get; set; }

        public RoleDto()
        {
        }

        public RoleDto(RoleEnum role)
        {
            Id = role.ToString();
            Label = role.DisplayName();
        }
    }
}
