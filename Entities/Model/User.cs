using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
    public class User : IEntity<string, User>
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; }

        public string PictureByte { get; set; }

        public string Token { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }

        public Role Role { get; set; }

        public bool Equals(User x, User y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(User obj)
        {
            return Id.GetHashCode();
        }
    }
}
