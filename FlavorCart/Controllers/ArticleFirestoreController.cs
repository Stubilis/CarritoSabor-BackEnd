
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class ArticleFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
 
    private readonly ArticleRepository _articleRepository = new();
    private UserTokenController _usertokenFirestoreController;

    public ArticleFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
    }

    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticlesAsync()
    {
        try
        {
            //Before returning the data, we need to verify the token
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            return Ok(await _articleRepository.GetAllAsync());
           
        }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        
        return BadRequest("No token found");
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Article>> GetArticlesAsync(string id)
    {
        //Before returning the data, we need to verify the token
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            var article = new Article()
            {
                Id = id
            };

            return Ok(await _articleRepository.GetAsync(article));
        }
        return BadRequest("Invalid token");
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateArticleAsync(string id, Article article)
    {
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            if (id != article.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _articleRepository.UpdateAsync(article));
        }
        return BadRequest("Invalid token");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteArticleAsync(string id, Article article)
    {
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            if (id != article.Id)
        {
            return BadRequest("Id must match.");
        }

        await _articleRepository.DeleteAsync(article);

        return Ok();
        }
        return BadRequest("Invalid token");
    }


    [HttpPost]
    public async Task<ActionResult<Article>> AddArticleAsync(Article article)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _articleRepository.AddAsync(article));
            }
        }
        catch (Exception e)
        {
            return BadRequest("No token found");
        }
        return BadRequest("Invalid token");
    }
    
    [HttpGet]
    [Route("category/{category}")]
    public async Task<ActionResult<Article>> GetArticleByCategory(string category)
    {
        try { 
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _articleRepository.GetArticleByCategory(category));
            }
        }
        catch (Exception e)
        {
            return BadRequest("No token found");
        }
       
            return BadRequest("Invalid token");
        }

   
}