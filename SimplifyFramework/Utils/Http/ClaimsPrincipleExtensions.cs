using System.Security.Claims;

namespace SimplifyScrum.Utils;

public static class ClaimsPrincipleExtensions
{
    public static string GetUserGuid(this ClaimsPrincipal user)
    {
        return user
            .Claims
            .First(c => c.Type == SimpleClaims.UserGuidClaim)
            .Value;
    }
    
    
    public static string? GetScrumClaim(this ClaimsPrincipal user)
    {
        return user
            .Claims
            .FirstOrDefault(c => c.Type == SimpleClaims.ScrumRoleClaim)
            .Value;
    }
}