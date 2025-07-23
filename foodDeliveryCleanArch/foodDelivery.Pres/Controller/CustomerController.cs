using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
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
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {e.Message}");
        }
    }

    [HttpGet("autocomplete/restaurant")]
    [Authorize]
    public IActionResult AutocompleteRestaurants(string prefix)
    {
        try
        {
            var response = customerService.AutocompleteRestaurants(prefix);
            return Ok(response);
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
}