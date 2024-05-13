
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class UserFirestoreController : ControllerBase
{
    //Create login method with google token

    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly UserRepository _userRepository = new();

    public UserFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<User>> GetUserAsync(string email)
    {
        var user = new User()
        {
            Email = email
        };

        return Ok(await _userRepository.GetUserByEmailAsync(user));
    }

    [HttpPut]
    [Route("{email}")]
    public async Task<ActionResult<User>> UpdateUserAsync(string email, User user)
    {
        if (email != user.Email)
        {
            return BadRequest("Email must match.");
        }

        return Ok(await _userRepository.UpdateByEmailAsync(user));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUserAsync(string id)
    {
       User user = new User()
       {
            Id = id
        };

        await _userRepository.DeleteAsync(user);

        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult<User>> AddUserAsync(User user)
    {
        return Ok(await _userRepository.AddAsync(user));
    }
  

}