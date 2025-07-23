using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.DTOs.Restaurant;
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
    
    [HttpPost("add/to-order")]
    [Authorize]
    public IActionResult AddToOrder(AddToOrderRequest request)
    {
        try
        {
            var response = customerService.AddToOrder(request);
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

    [HttpPatch("set/order-quantity")]
    [Authorize]
    public IActionResult SetOrderQuantity(SetOrderQuantityDto request)
    {
        try
        {
            customerService.SetOrderQuantity(request);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { error = e.Message });
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {e.Message}");
        }
    }
}