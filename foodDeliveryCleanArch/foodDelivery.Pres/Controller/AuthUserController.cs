using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("authUserController")]
public class AuthUserController(IAuthUserService authUserService) : ControllerBase
{
    [HttpGet("autocomplete/restaurants")]
    [Authorize]
    public IActionResult AutocompleteRestaurants(string prefix)
    {
        try
        {
            var response = authUserService.AutocompleteRestaurants(prefix);
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
    public IActionResult GetMenu(Guid menuId)
    {
        try
        {
            var response = authUserService.GetMenu(menuId);
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
    public IActionResult GetMenus(Guid restaurantId)
    {
        try
        {
            var response = authUserService.GetMenus(restaurantId);
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
    public IActionResult AutocompleteFoods(Guid restaurantId, string prefix)
    {
        try
        {
            var response = authUserService.AutocompleteFoods(restaurantId, prefix);
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
}