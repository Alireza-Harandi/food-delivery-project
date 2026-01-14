using foodDelivery.Application.DTOs.User;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Presentation.Controller;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var response = await userService.LoginAsync(request);
        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await userService.LogoutAsync();
        return NoContent();
    }
}