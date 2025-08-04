using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("restaurant")]
public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpPost("add/menu")]
    [Authorize]
    public async Task<IActionResult> AddMenu([FromBody] AddMenuRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await restaurantService.AddMenuAsync(request);
        return Ok(response);
    }

    [HttpPost("add/food")]
    [Authorize]
    public async Task<IActionResult> AddFood([FromBody] AddFoodRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await restaurantService.AddFoodAsync(request);
        return Ok(response);
    }

    [HttpDelete("remove/menu{menuId}-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> DeleteMenu(Guid menuId, Guid restaurantId)
    {
        await restaurantService.DeleteMenuAsync(menuId, restaurantId);
        return NoContent();
    }

    [HttpDelete("remove/food{foodId}-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> DeleteFood(Guid foodId, Guid restaurantId)
    {
        await restaurantService.DeleteFoodAsync(foodId, restaurantId);
        return NoContent();
    }

    [HttpPatch("set/stock-{foodId}-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> SetFoodStock(Guid restaurantId, Guid foodId, [FromBody] UpdateStockDto request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        await restaurantService.SetFoodStockAsync(restaurantId, foodId, request);
        return NoContent();
    }

    [HttpPut("set/location")]
    [Authorize]
    public async Task<IActionResult> SetLocation([FromBody] SetLocationRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await restaurantService.SetLocationAsync(request);
        return Ok(response);
    }

    [HttpPut("set/working-hours")]
    [Authorize]
    public async Task<IActionResult> SetWorkingHours([FromBody] SetWhRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await restaurantService.SetWhAsync(request);
        return Ok(response);
    }

    [HttpGet("profile/restaurant/{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> GetFinalizedOrders(Guid restaurantId)
    {
        var response = await restaurantService.GetFinalizedOrdersAsync(restaurantId);
        return Ok(response);
    }
}