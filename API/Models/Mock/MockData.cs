using System.Collections.Generic;
using Core;
using Entities.Model;

namespace API.Models.Mock
{
    public class MockData
    {
        public static readonly List<Role> Roles = new List<Role>()
        {
            new Role(RoleEnum.Default),
            new Role(RoleEnum.Admin),
            new Role(RoleEnum.None),
        };

        public static readonly List<User> Users = new List<User>()
        {
            new User() { FirstName = "Toto", LastName = "Titi", RoleId = RoleEnum.Default.ToString() },
            new User() { FirstName = "Jérémy", LastName = "Talabard", RoleId = RoleEnum.Admin.ToString() },
        };
    }
}
