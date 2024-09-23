using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
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
    public async Task<IActionResult> Login([FromBody] SimpleUserModel userModel)
    {
        var result = await securityManager.Login(userModel);

        if (result.IsSuccess)
        {
            userModel = userModel with { Id = await securityManager.GetLoggedUsersGuid() };
            var claims = CreateClaims(userModel);
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
    public async Task<IActionResult> SignIn([FromBody] SimpleUserModel userModel)
    {
        var signInResult = await securityManager.SignIn(userModel);

        if (signInResult.IsSuccess)
        {
            userModel = userModel with { Id = await securityManager.GetLoggedUsersGuid() };
            var claims = CreateClaims(userModel);
            var token = securityManager.GetToken(claims);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        
        return StatusCode(500, signInResult.Exception!.Message);

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

    private List<Claim> CreateClaims(SimpleUserModel userModel)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userModel.Username),
            new (SimpleClaims.UserGuidClaim, userModel.Id)
        };
        
        return claims;
    }
}