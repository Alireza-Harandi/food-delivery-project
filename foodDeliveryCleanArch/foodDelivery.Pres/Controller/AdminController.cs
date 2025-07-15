using foodDelivery.Application.DTOs.Admin;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("admin")]
public class AdminController(IAdminService adminService) : ControllerBase
{
    [Authorize]
    [HttpPost("signup")]
    public IActionResult Signup([FromBody] AdminSignupRequest request)
    {
        try
        {
            var response = adminService.Signup(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { error = e.Message });
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}