using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModule.Abstraction;
using UserModule.Records;

namespace SimplifyScrum.Controllers.User;

[ApiController]
[Route("api/v1/scrum/")]
public class LoginController(IManageSecurity securityManager) : ControllerBase
{
    
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AppUser user)
    {
        var result = await securityManager.Login(user);

        if (result.IsSuccess)
        {
            var claims = CreateClaims(user);
            var token = securityManager.GetToken(claims);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return Unauthorized(result.Exception!.Message);
    }
    
    [HttpGet]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var result = await securityManager.Logout();
            
        if(result.IsSuccess)
            return Ok();

        return StatusCode(500, result.Exception!.Message);
    }

    [HttpPost]
    [Route("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] AppUser user)
    {
        var result = await securityManager.SignIn(user);

        if (result.IsSuccess)
        {
            var claims = CreateClaims(user);
            var token = securityManager.GetToken(claims);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        

        return StatusCode(500, result.Exception!.Message);
    }

    [HttpDelete]
    [Route("delete")]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        var result = await securityManager.Delete();

        if (result.IsSuccess)
            return Ok();

        return StatusCode(500, result.Exception!.Message);
    }

    private List<Claim> CreateClaims(AppUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Username),
        };
        
        return claims;
    }
}