
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
    

}