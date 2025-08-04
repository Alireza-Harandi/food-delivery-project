using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArgumentException = System.ArgumentException;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("customer")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] CustomerSignupRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await customerService.SignupAsync(request);
        return Ok(response);
    }

    [HttpPost("add/to-order")]
    [Authorize]
    public async Task<IActionResult> AddToOrder([FromBody] AddToOrderRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await customerService.AddToOrderAsync(request);
        return Ok(response);
    }

    [HttpPatch("set/order-quantity")]
    [Authorize]
    public async Task<IActionResult> SetOrderQuantity([FromBody] SetOrderQuantityDto request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        await customerService.SetOrderQuantityAsync(request);
        return NoContent();
    }

    [HttpGet("orders-{orderId}")]
    [Authorize]
    public async Task<IActionResult> GetOrders(Guid orderId)
    {
        var response = await customerService.GetOrdersAsync(orderId);
        return Ok(response);
    }


    [HttpGet("finalize/order")]
    [Authorize]
    public async Task<IActionResult> FinalizeOrder([FromBody] FinalizeOrderRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await customerService.FinalizeOrderAsync(request);
        return Ok(response);
    }

    [HttpPost("report")]
    [Authorize]
    public async Task<IActionResult> ReportRestaurant([FromBody] ReportRestaurantDto request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        await customerService.ReportRestaurantAsync(request);
        return NoContent();
    }

    [HttpDelete("delete/order-{orderId}")]
    [Authorize]
    public async Task<IActionResult> DeleteOrder(Guid orderId)
    {
        await customerService.DeleteOrderAsync(orderId);
        return NoContent();
    }

    [HttpPost("submit/rating")]
    [Authorize]
    public async Task<IActionResult> SubmitRating([FromBody] SubmitRatingDto request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        await customerService.SubmitRatingAsync(request);
        return NoContent();
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var response = await customerService.GetProfileAsync();
        return Ok(response);
    }
}