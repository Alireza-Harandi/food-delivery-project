using foodDelivery.Application.DTOs.Admin;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("admin")]
public class AdminController(IAdminService adminService) : ControllerBase
{
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] AdminSignupRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await adminService.SignupAsync(request);
        return Ok(response);
    }

    [Authorize(Roles = nameof(Role.Admin))]
    [HttpGet("reports")]
    public async Task<IActionResult> GetReports()
    {
        var response = await adminService.GetReportsAsync();
        return Ok(response);
    }
}