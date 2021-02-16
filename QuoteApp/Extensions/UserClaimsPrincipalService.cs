using Quote.Utils;
using System;
using System.Security.Claims;

namespace QuoteServer.Extensions
{
    public static class UserClaimsPrincipalService
    {
        public static bool IsRoleAdmin(this ClaimsPrincipal identity)
        {
            var role = identity.FindFirst(ClaimTypes.Role);
            return role.Value.Contains("999;");
        }

        public static Guid GetId(this ClaimsPrincipal identity)
        {
            var role = identity.FindFirst(ClaimTypes.Sid);
            return role.Value.ToGuid();
        }
        
        public static string GetAccess(this ClaimsPrincipal identity)
        {
            var role = identity.FindFirst(ClaimTypes.Role);
            return role.Value;
        }
    }
}
