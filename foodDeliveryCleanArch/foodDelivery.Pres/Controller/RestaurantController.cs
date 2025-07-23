using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("restaurant")]
public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpPost("add/menu")]
    [Authorize]
    public IActionResult AddMenu([FromBody] AddMenuRequest request)
    {
        try
        {
            AddMenuResponse response = restaurantService.AddMenu(request);
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
    public IActionResult AddFood([FromBody] AddFoodRequest request)
    {
        try
        {
            AddFoodResponse response = restaurantService.AddFood(request);
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
    public IActionResult DeleteMenu(Guid menuId, Guid restaurantId)
    {
        try
        {
            restaurantService.DeleteMenu(menuId, restaurantId);
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
    public IActionResult DeleteFood(Guid foodId, Guid restaurantId)
    {
        try
        {
            restaurantService.DeleteFood(foodId, restaurantId);
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
    
    [HttpGet("menu{menuId}-{restaurantId}")]
    [Authorize]
    public IActionResult GetMenu(Guid menuId, Guid restaurantId)
    {
        try
        {
            var response = restaurantService.GetMenu(restaurantId, menuId);
            return Ok(response);
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
    public IActionResult SetFoodStock(Guid restaurantId, Guid foodId, [FromBody] UpdateStockDto request)
    {
        try
        {
            restaurantService.SetFoodStock(restaurantId, foodId, request);
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
    public IActionResult SetLocation([FromBody] SetLocationRequest request)
    {
        try
        {
            var response = restaurantService.SetLocation(request);
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
    public IActionResult SetWorkingHours([FromBody] SetWhRequest request)
    {
        try
        {
            SetWhResponse response = restaurantService.SetWh(request);
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
    
    [HttpGet("menus-{restaurantId}")]
    [Authorize]
    public IActionResult GetMenus(Guid restaurantId)
    {
        try
        {
            var response = restaurantService.GetMenus(restaurantId);
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