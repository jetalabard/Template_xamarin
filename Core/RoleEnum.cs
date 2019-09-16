using System;
using System.ComponentModel;

namespace Core
{
    [Flags]
    public enum RoleEnum
    {
        /// <summary>
        /// nothing role.
        /// </summary>
        [Description("Aucun")]
        None,

        /// <summary>
        /// default role.
        /// </summary>
        [Description("Défaut")]
        Default,

        /// <summary>
        /// admin role.
        /// </summary>
        [Description("Administrateur")]
        Admin,
    }
}
