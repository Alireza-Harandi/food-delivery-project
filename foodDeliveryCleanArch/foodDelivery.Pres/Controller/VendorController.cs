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
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
    
    [HttpPatch("set/location")]
    [Authorize]
    public IActionResult SetLocation([FromBody] VendorSetLocationRequest request)
    {
        try
        {
            var response = vendorService.SetLocation(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred");
        }
    }

    
}