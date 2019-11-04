using System.Collections.Generic;
using Core;
using Core.Helpers;
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

        private static (byte[], byte[]) GetPassword()
        {
            new HashService().CreatePasswordHash("Admin123!", out byte[] passwordHash, out byte[] passwordSalt);
            return (passwordHash, passwordSalt);
        }

        public static readonly List<User> Users = new List<User>()
        {
            new User() { FirstName = "Toto", LastName = "Titi", RoleId = RoleEnum.Default.ToString(), PasswordHash = GetPassword().Item1, PasswordSalt = GetPassword().Item2 },
            new User() { FirstName = "Jérémy", LastName = "Talabard", RoleId = RoleEnum.Admin.ToString(), PasswordHash = GetPassword().Item1, PasswordSalt = GetPassword().Item2 },
        };
    }
}
