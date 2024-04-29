
using FlavorCart.Models;
using FlavorCart.Repositories;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class UserTokenFirestoreController : ControllerBase
{
    //Create login method with google token

    private readonly ILogger<UserTokenFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly UserTokenRepository _userTokenRepository = new();

    public UserTokenFirestoreController(ILogger<UserTokenFirestoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserToken>>> GetAllUsersAsync()
    {
        return Ok(await _userTokenRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<UserToken>> GetUserAsync(string id)
    {
        var user = new UserToken()
        {
            Id = id
        };

        return Ok(await _userTokenRepository.GetAsync(user));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateUserAsync(string id, UserToken user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _userTokenRepository.UpdateAsync(user));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUserAsync(string id, UserToken user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id must match.");
        }

        await _userTokenRepository.DeleteAsync(user);

        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult<User>> AddUserAsync(UserToken user)
    {
        
        return Ok(await _userTokenRepository.AddAsync(user));
    }
    [AllowAnonymous]
    [HttpPost("verify")]
    public async Task<ActionResult> Verify()
    {
        string token = Request.Headers["Authorization"].ToString().Remove(0, 7); //remove Bearer
        var payload = await VerifyGoogleTokenId(token);
        if (payload == null)
        {
            return BadRequest("Invalid token");
        }


        return Ok(payload);
    }
    [HttpPost]
    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenId(string token)
    {

        try
        {

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token);

            return payload;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid google token");

        }

        return null;

    }

}