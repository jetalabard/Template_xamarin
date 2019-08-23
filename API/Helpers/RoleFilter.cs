using System;
using System.Security.Claims;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public sealed class RoleFilter : IAuthorizationFilter
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
