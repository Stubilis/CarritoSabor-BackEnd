
using FlavorCart.Models;
using FlavorCart.Repositories;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class ArticleFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly ArticleRepository _articleRepository = new();

    public ArticleFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticlesAsync()
    {
        return Ok(await _articleRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Article>> GetArticlesAsync(string id)
    {
        var article = new Article()
        {
            Id = id
        };

        return Ok(await _articleRepository.GetAsync(article));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateArticleAsync(string id, Article article)
    {
        if (id != article.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _articleRepository.UpdateAsync(article));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteArticleAsync(string id, Article article)
    {
        if (id != article.Id)
        {
            return BadRequest("Id must match.");
        }

        await _articleRepository.DeleteAsync(article);

        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult<Article>> AddArticleAsync(Article article)
    {
        return Ok(await _articleRepository.AddAsync(article));
    }
    
    [HttpGet]
    [Route("category/{category}")]
    public async Task<ActionResult<Article>> GetArticleByCategory(string category)
    {
        return Ok(await _articleRepository.GetArticleByCategory(category));
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
        /*
        try
        {
            // uncomment these lines if you want to add settings: 
            // var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            // { 
            //     Audience = new string[] { "yourServerClientIdFromGoogleConsole.apps.googleusercontent.com" }
            // };
            // Add your settings and then get the payload
            // GoogleJsonWebSignature.Payload payload =  await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);

            // Or Get the payload without settings.
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token);

            return payload;
        }
        catch (System.Exception)
        {
            Console.WriteLine("invalid google token");

        }
        */
        return null;
        
    }
}