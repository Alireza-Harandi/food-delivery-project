using foodDelivery.Application.DTOs.Admin;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("admin")]
public class AdminController(IAdminService adminService) : ControllerBase
{
    [Authorize]
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] AdminSignupRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        try
        {
            AdminSignupResponse response = await adminService.SignupAsync(request);
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
    public async Task<IActionResult> GetReports()
    {
        try
        {
            ReportsDto response = await adminService.GetReportsAsync();
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