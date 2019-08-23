using Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Extensions;

namespace Entities.Model
{
    public class Role : IEntity<string, Role>
    {
        public Role()
        {

        }

        public Role(RoleEnum permission)
        {
            Id = permission.ToString();
            Label = permission.DisplayName();
        }

        [Key]
        public string Id { get; set; }
        
        public string Label { get; set; }

        public bool Equals(Role x, Role y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Role obj)
        {
            return Id.GetHashCode();
        }
    }
}
