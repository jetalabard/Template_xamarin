﻿namespace Core.Dto
{
    public class UserDto : Dto
    {
        public string PersonalId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string PictureByte { get; set; }

        public string Token { get; set; }

        public RoleDto Role { get; set; }

        public string RoleId { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
