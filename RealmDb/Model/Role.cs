using Core;
using Core.Extensions;
using Realms;

namespace RealmDb.Model
{
    public class Role : RealmObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="permission">Enum to set Role.</param>
        public Role(RoleEnum permission)
        {
            Id = permission.ToString();
            Label = permission.DisplayName();
        }

        [PrimaryKey]
        public string Id { get; set; }

        public string Label { get; set; }
    }
}
