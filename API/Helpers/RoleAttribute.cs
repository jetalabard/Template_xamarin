using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;

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

    internal sealed class RoleFilter : IAuthorizationFilter
    {
        private readonly RoleEnum _permission;

        public RoleFilter(RoleEnum permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionString = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            Enum.TryParse(permissionString, out RoleEnum role);

            if (!role.HasFlag(_permission))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
