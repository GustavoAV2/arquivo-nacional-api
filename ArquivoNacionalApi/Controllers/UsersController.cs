using Microsoft.AspNetCore.Mvc;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Services;

namespace ArquivoNacionalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public ActionResult CreateUser(UserDTO userDto)
    {
        _userService.CreateUser(userDto);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
    {
        return Ok(await _userService.GetUserByIdAsync(id));
    }

    [HttpGet()]
    public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }
}