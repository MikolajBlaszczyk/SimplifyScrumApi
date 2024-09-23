using System.Security.Claims;

namespace SimplifyScrum.Utils;

internal static class ClaimsPrincipleExtensions
{
    internal static string GetUserGuid(this ClaimsPrincipal user)
    {
        return user
            .Claims
            .First(c => c.Type == SimpleClaims.UserGuidClaim)
            .Value;
    }
}