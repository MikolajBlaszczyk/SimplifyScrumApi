using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimplifyScrum.Utils;

public static class ApiControllerExtensions
{
    //Creat method for permantent registering claims
    public static async Task RegisterClaim(this ControllerBase controller, string claimType, string value)
    {
        var identity = controller.User.Identity as ClaimsIdentity;
        identity.AddClaim(new Claim(claimType, value));
        await controller.HttpContext.SignInAsync(controller.HttpContext.User);
    }
}