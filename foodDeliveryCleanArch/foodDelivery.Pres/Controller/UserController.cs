using foodDelivery.Application.DTOs.User;
using foodDelivery.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace foodDelivery.Pres.Controller;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest request)
    {
        try
        {
            var response = userService.Login(request);
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
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            userService.Logout();
            return NoContent();
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { error = e.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred\\the following error: {ex.Message}");
        }
    }
}