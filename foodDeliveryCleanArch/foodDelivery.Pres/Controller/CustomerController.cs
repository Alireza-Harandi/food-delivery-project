using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArgumentException = System.ArgumentException;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("customer")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] CustomerSignupRequest request)
    {
        try
        {
            var response = await customerService.SignupAsync(request);
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
    public async Task<IActionResult> AddToOrder(AddToOrderRequest request)
    {
        try
        {
            var response = await customerService.AddToOrderAsync(request);
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
    public async Task<IActionResult> SetOrderQuantity(SetOrderQuantityDto request)
    {
        try
        {
            await customerService.SetOrderQuantityAsync(request);
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

    [HttpGet("orders-{orderId}")]
    [Authorize]
    public async Task<IActionResult> GetOrders(Guid orderId)
    {
        try
        {
            CustomerOrderDto response = await customerService.GetOrdersAsync(orderId);
            return Ok(response);
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


    [HttpGet("finalize/order-{orderId}")]
    [Authorize]
    public async Task<IActionResult> FinalizeOrder(FinalizeOrderRequest request)
    {
        try
        {
            FinalizeOrderResponse response = await customerService.FinalizeOrderAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { error = e.Message });
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { error = e.Message });
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

    [HttpPost("report")]
    [Authorize]
    public async Task<IActionResult> ReportRestaurant(ReportRestaurantDto request)
    {
        try
        {
            await customerService.ReportRestaurantAsync(request);
            return NoContent();
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

    [HttpDelete("delete/order-{orderId}")]
    [Authorize]
    public async Task<IActionResult> DeleteOrder(Guid orderId)
    {
        try
        {
            await customerService.DeleteOrderAsync(orderId);
            return NoContent();
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

    [HttpPost("submit/rating")]
    [Authorize]
    public async Task<IActionResult> SubmitRating(SubmitRatingDto request)
    {
        try
        {
            await customerService.SubmitRatingAsync(request);
            return NoContent();
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

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            CustomerProfileDto response = await customerService.GetProfileAsync();
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