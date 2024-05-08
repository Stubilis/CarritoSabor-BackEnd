
using FlavorCart.Models;
using FlavorCart.Repositories;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class UserTokenController : ControllerBase
{
   

    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected 
    private readonly UserTokenRepository _userTokenRepository = new();

    public UserTokenController(ILogger<UserFirestoreController> logger)
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
    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<UserToken>> GetUserByEmailAsync(string email)
    {
        var user = new UserToken()
        {
            Email = email
        };

        return Ok(await _userTokenRepository.GetUserByEmailAsync(user));
    }

    [HttpPut]
    [Route("{email}")]
    public async Task<ActionResult<User>> UpdateUserAsync(UserToken user)
    {
      

        return Ok(await _userTokenRepository.UpdateAsyncByEmail(user));
    }

    [HttpDelete]
    [Route("{email}")]
    public async Task<ActionResult> DeleteUserAsync(string email, UserToken user)
    {
        if (email != user.Email)
        {
            return BadRequest("Email must match.");
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
    public async Task<ActionResult> Verify(string token)
    {
            var payload = await VerifyGoogleTokenId(token);
            if (payload == null)
            {
                BadRequest("Invalid token");
                return null;
            }
            try
            {
                //Check if the user is already in the database
                var user = new UserToken()
                {
                    Email = payload.Email,
                   
                };
                var result = await _userTokenRepository.GetUserByEmailAsync(user);
                if (result != null)
                {
                //Change the token and expiration time
                result[0].Token = token;
                result[0].ExpirationTimeSeconds = (long)payload.ExpirationTimeSeconds;
                    //Update the token
                    await _userTokenRepository.UpdateAsync(result);
                }
            }
            catch (Exception)
            {
            //BadRequest("User not found");
            //Add the user to the database
            var res = await _userTokenRepository.AddAsync(new UserToken() { Email = payload.Email, Token = token, ExpirationTimeSeconds = (long)payload.ExpirationTimeSeconds });
            }
            
            Console.WriteLine(payload);
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