
using FlavorCart.Models;
using FlavorCart.Repositories;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Linq.Expressions;


namespace Firestore.Controllers;

public class UserTokenController : ControllerBase
{
   

    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected 
    private readonly UserTokenRepository _userTokenRepository = new();
    private readonly UserRepository _userRepository = new();

    public UserTokenController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
    }

   
    public async Task<ActionResult<List<UserToken>>> GetAllUsersAsync()
    {
        return Ok(await _userTokenRepository.GetAllAsync());
    }

   
  
    public async Task<ActionResult<UserToken>> GetUserByEmailAsync(string email)
    {
        var user = new UserToken()
        {
            Email = email
        };

        return Ok(await _userTokenRepository.GetUserByEmailAsync(user));
    }

   
    public async Task<ActionResult<User>> UpdateUserAsync(UserToken user)
    {
      

        return Ok(await _userTokenRepository.UpdateAsyncByEmail(user));
    }

   
    public async Task<ActionResult> DeleteUserAsync(string email)
    {
        UserToken user = new UserToken()
        {
            Email = email
        };

        await _userTokenRepository.DeleteAsync(user);

        return Ok("Deleted");
    }


    public async Task<ActionResult<User>> AddUserAsync(UserToken user)
    {
       
        return Ok(await _userTokenRepository.AddAsync(user));
    }

    
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
            var _user = new UserToken()
            {
                Email = payload.Email,

            };
           
            
            var result = await _userTokenRepository.GetUserByEmailAsync(_user);
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
            //Add the user to the database
            var res = await _userTokenRepository.AddAsync(new UserToken() { Email = payload.Email, Token = token, ExpirationTimeSeconds = (long)payload.ExpirationTimeSeconds });
            return Ok("User Saved");
        }
        var user = new User()
        {
            Name = payload.Name,
            Email = payload.Email,
            Nickname = "@" + payload.GivenName.ToLower(),
            Languaje ="ES"

        };
        //Now check if the user exists in the user collection
        await VerifyUserByEmail(user);
            
          //  Console.WriteLine(payload);
            return Ok("Valid token");

      
    }
    //Method to verify that the user exists in the user collection
    //If the user exists, return the OK
    //If the user does not exist, save the user in the user collection
    private async Task<ActionResult> VerifyUserByEmail(User user)
    {
        try
        {


            var result = await _userRepository.GetUserByEmailAsync(user);
            if (result.Count > 0)
            {
                return Ok("User exists");
            }
            else
            {
                   await _userRepository.AddAsync(user);
                return Ok("User saved");
            
            }
        }
        catch (Exception)
        {
            await _userRepository.AddAsync(user);
            return Ok("User saved");
        }
        return BadRequest("User not found");
    }
    

  
    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenId(string token)
    {

        try {

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