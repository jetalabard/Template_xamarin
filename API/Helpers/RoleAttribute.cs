using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
    public class RoleAttribute : TypeFilterAttribute
    {
        public RoleAttribute(RoleEnum permission)
           : base(typeof(RoleFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
