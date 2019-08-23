using Core.Extensions;

namespace Core.Dto
{
    public class RoleDto
    {
        public string Code { get; set; }

        public string Label { get; set; }

        public RoleDto()
        {

        }

        public RoleDto(RoleEnum role)
        {
            Code = role.ToString();
            Label = role.DisplayName();
        }
    }
}
