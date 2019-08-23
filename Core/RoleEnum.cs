using System;
using System.ComponentModel;

namespace Core
{
    [Flags]
    public enum RoleEnum
    {
        [Description("Aucun")]
        None,
        [Description("Défaut")]
        Default,
        [Description("Administrateur")]
        Admin,
    }
}
