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
        try
        {
            AddMenuResponse response = await restaurantService.AddMenuAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpPost("add/food")]
    [Authorize]
    public async Task<IActionResult> AddFood([FromBody] AddFoodRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        try
        {
            AddFoodResponse response = await restaurantService.AddFoodAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpDelete("remove/menu{menuId}-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> DeleteMenu(Guid menuId, Guid restaurantId)
    {
        try
        {
            await restaurantService.DeleteMenuAsync(menuId, restaurantId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpDelete("remove/food{foodId}-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> DeleteFood(Guid foodId, Guid restaurantId)
    {
        try
        {
            await restaurantService.DeleteFoodAsync(foodId, restaurantId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpPatch("set/stock-{foodId}-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> SetFoodStock(Guid restaurantId, Guid foodId, [FromBody] UpdateStockDto request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        try
        {
            await restaurantService.SetFoodStockAsync(restaurantId, foodId, request);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpPut("set/location")]
    [Authorize]
    public async Task<IActionResult> SetLocation([FromBody] SetLocationRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        try
        {
            var response = await restaurantService.SetLocationAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpPut("set/working-hours")]
    [Authorize]
    public async Task<IActionResult> SetWorkingHours([FromBody] SetWhRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        try
        {
            SetWhResponse response = await restaurantService.SetWhAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }

    [HttpGet("profile/restaurant/{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> GetFinalizedOrders(Guid restaurantId)
    {
        try
        {
            RestaurantOrderDto response = await restaurantService.GetFinalizedOrdersAsync(restaurantId);
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