using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimplifyScrum.Controllers.Dashboard;

[ApiController]
[Authorize]
[Route("api/v1/scrum/meetings/")]
public class BacklogController : ControllerBase
{
   
}