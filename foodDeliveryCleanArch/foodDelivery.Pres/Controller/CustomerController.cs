using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("customer")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost("signup")]
    public IActionResult Signup([FromBody] CustomerSignupRequest request)
    {
        try
        {
            var response = customerService.Signup(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] CustomerLoginRequest request)
    {
        try
        {
            var response = customerService.Login(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return Unauthorized(new { error = e.Message });
        }
    }
}