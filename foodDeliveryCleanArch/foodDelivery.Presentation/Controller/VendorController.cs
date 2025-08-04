using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("vendor")]
public class VendorController(IVendorService vendorService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] VendorSignupRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await vendorService.SignupAsync(request);
        return Ok(response);
    }

    [HttpPost("register/restaurant")]
    [Authorize]
    public async Task<IActionResult> RegisterRestaurant([FromBody] RegisterRestaurantRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await vendorService.RegisterRestaurantAsync(request);
        return Ok(response);
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var response = await vendorService.GetProfileAsync();
        return Ok(response);
    }
}