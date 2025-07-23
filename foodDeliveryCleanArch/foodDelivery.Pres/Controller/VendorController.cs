using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using foodDelivery.Infrustructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("vendor")]
public class VendorController(IVendorService vendorService) : ControllerBase
{
    [HttpPost("signup")]
    public IActionResult Signup([FromBody] VendorSignupRequest request)
    {
        try
        {
            var response = vendorService.Signup(request);
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
    public IActionResult RegisterRestaurant([FromBody] RegisterRestaurantRequest request)
    {
        try
        {
            var response = vendorService.RegisterRestaurant(request);
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
    public IActionResult GetProfile()
    {
        try
        {
            var response = vendorService.GetProfile();
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