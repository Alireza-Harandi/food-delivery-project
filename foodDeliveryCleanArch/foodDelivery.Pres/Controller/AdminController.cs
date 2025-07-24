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
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {e.Message}");
        }
    }

    [Authorize]
    [HttpGet("reports")]
    public IActionResult GetReports()
    {
        try
        {
            ReportsDto response = adminService.GetReports();
            return Ok(response);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {e.Message}");
        }
    }
}