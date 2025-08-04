using foodDelivery.Application.DTOs.AuthUser;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("authUserController")]
public class AuthUserController(IAuthUserService authUserService) : ControllerBase
{
    [HttpGet("autocomplete/restaurants")]
    [Authorize]
    public async Task<IActionResult> AutocompleteRestaurants(string prefix)
    {
        try
        {
            var response = await authUserService.AutocompleteRestaurantsAsync(prefix);
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

    [HttpGet("menu-{menuId}")]
    [Authorize]
    public async Task<IActionResult> GetMenu(Guid menuId)
    {
        try
        {
            var response = await authUserService.GetMenuAsync(menuId);
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

    [HttpGet("menus-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> GetMenus(Guid restaurantId)
    {
        try
        {
            var response = await authUserService.GetMenusAsync(restaurantId);
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

    [HttpGet("autocomplete/foods")]
    [Authorize]
    public async Task<IActionResult> AutocompleteFoods(Guid restaurantId, string prefix)
    {
        try
        {
            var response = await authUserService.AutocompleteFoodsAsync(restaurantId, prefix);
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

    [HttpGet("profile/restaurant-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> GetRestaurantProfile(Guid restaurantId)
    {
        try
        {
            RestaurantProfileDto response = await authUserService.GetRestaurantProfileAsync(restaurantId);
            return Ok(response);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { error = e.Message });
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