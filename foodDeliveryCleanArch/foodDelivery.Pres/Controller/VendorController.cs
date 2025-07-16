using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using foodDelivery.Infrustructure;
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
}