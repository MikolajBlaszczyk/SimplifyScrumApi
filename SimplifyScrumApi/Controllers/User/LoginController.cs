using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplifyScrum.Utils;
using UserModule.Abstraction;
using UserModule.Informations;
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
        var result = await securityManager.LoginAsync(userModel);

        if (result.IsSuccess)
        {
            userModel = userModel with { Id = await securityManager.GetLoggedUsersGUIDAsync() };
            var roles = (await securityManager.GetAllUserRoles(userModel.Id)).Data as List<string>;
            var claims = CreateClaims(userModel, roles);
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
        var result = await securityManager.LogoutAsync();
            
        if(result.IsSuccess)
            return Ok();

        return StatusCode(500, result.Exception!.Message);
    }

    [HttpPost]
    [Route("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SimpleUserModel userModel)
    {
        var signInResult = await securityManager.SignInAsync(userModel);
       
        if (signInResult.IsSuccess)
        {
            await securityManager.AddRoleAsyncForCurrentUser(SystemRole.User);
            userModel = userModel with { Id = await securityManager.GetLoggedUsersGUIDAsync() };
            var claims = CreateClaims(userModel, new List<string>{ SystemRole.User });
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
        var result = await securityManager.DeleteAsync();

        if (result.IsSuccess)
            return Ok();

        return StatusCode(500, result.Exception!.Message);
    }

    public class AddRoleBody
    {
        public SimpleUserModel user { get; set; }
        public string role { get; set; }
    }
    
    [HttpPost]
    [Route("addrole")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<IActionResult> AddRoleForUser([FromBody] AddRoleBody body)
    {
        var result = await securityManager.AddRoleForUser(body.user, body.role);

        if (result.IsSuccess)
            return Ok();
        
        return StatusCode(500, result.Exception!.Message);
    }

    [HttpGet]
    [Route("isadmin")]
    [Authorize(Roles = SystemRole.Admin)]
    public async Task<IActionResult> IsAdmin()
    {
        var isAdmin = User.IsInRole(SystemRole.Admin);

        if (isAdmin)
            return Ok();
      
        return BadRequest();
    }
    
    private List<Claim> CreateClaims(SimpleUserModel userModel, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userModel.Username),
            new (SimpleClaims.UserGuidClaim, userModel.Id)
        };

        foreach (var role in roles)
        {
            claims.Add(new (ClaimTypes.Role, role));
        }
        
        return claims;
    }
}