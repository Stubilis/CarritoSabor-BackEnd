
using FlavorCart.Models;
using FlavorCart.Repositories;
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
        var user = new Article()
        {
            Id = id
        };

        return Ok(await _articleRepository.GetAsync(user));
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateArticleAsync(string id, Article user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _articleRepository.UpdateAsync(user));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteArticleAsync(string id, Article user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id must match.");
        }

        await _articleRepository.DeleteAsync(user);

        return Ok();
    }


    [HttpPut]
    public async Task<ActionResult<Article>> DeleteArticleAsync(Article user)
    {
        return Ok(await _articleRepository.AddAsync(user));
    }
    /*
    [HttpGet]
    [Route("city/{city}")]
    public async Task<ActionResult<User>> GetUserWhereCity(string city)
    {
        return Ok(await _userRepository.GetUserWhereCity(city));
    }
    */

}