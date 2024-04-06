using System.Security.Claims;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Web.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            string? userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return userId;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            bool result = user.IsInRole(AdminUser.AdminRoleName);

            return result;
        }
    }
}
