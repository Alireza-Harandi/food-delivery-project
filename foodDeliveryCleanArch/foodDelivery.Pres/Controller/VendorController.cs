using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("vendor")]
public class VendorController(IVendorService vendorService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] VendorSignupRequest request)
    {
        try
        {
            var response = await vendorService.SignupAsync(request);
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { error = e.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpPost("register/restaurant")]
    [Authorize]
    public async Task<IActionResult> RegisterRestaurant([FromBody] RegisterRestaurantRequest request)
    {
        try
        {
            var response = await vendorService.RegisterRestaurantAsync(request);
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
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

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var response = await vendorService.GetProfileAsync();
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }
}