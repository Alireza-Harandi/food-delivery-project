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
        var response = await authUserService.AutocompleteRestaurantsAsync(prefix);
        return Ok(response);
    }

    [HttpGet("menu-{menuId}")]
    [Authorize]
    public async Task<IActionResult> GetMenu(Guid menuId)
    {
        var response = await authUserService.GetMenuAsync(menuId);
        return Ok(response);
    }

    [HttpGet("menus-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> GetMenus(Guid restaurantId)
    {
        var response = await authUserService.GetMenusAsync(restaurantId);
        return Ok(response);
    }

    [HttpGet("autocomplete/foods")]
    [Authorize]
    public async Task<IActionResult> AutocompleteFoods(Guid restaurantId, string prefix)
    {
        var response = await authUserService.AutocompleteFoodsAsync(restaurantId, prefix);
        return Ok(response);
    }

    [HttpGet("profile/restaurant-{restaurantId}")]
    [Authorize]
    public async Task<IActionResult> GetRestaurantProfile(Guid restaurantId)
    {
        var response = await authUserService.GetRestaurantProfileAsync(restaurantId);
        return Ok(response);
    }
}