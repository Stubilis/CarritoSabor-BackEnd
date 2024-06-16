
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class ArticleFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;

    private readonly ArticleRepository _articleRepository = new();
    private PriceRepository _priceRepository = new();
    private UserTokenController _usertokenFirestoreController;

    public ArticleFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
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
        try{
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            if (id != article.Id)
            {
                return BadRequest("Id must match.");
            }

            return Ok(await _articleRepository.UpdateAsync(article));
        }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteArticleAsync(string id)
    {
        try
        {
            var article = new Article()
            {
                Id = id
            };
            //Before returning the data, we need to verify the token
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                if (id != article.Id)
                {
                    return BadRequest("Id must match.");
                }

                await _articleRepository.DeleteAsync(article);
                // Also delete the prices
                await _priceRepository.DeletePricesByArticle(article.Id);

                return Ok("Deleted");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
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
                if (article.Categories.IsNullOrEmpty())
                {
                    // Add the default category
                    article.Categories.Add("vmnxxvN628gfp4WVbgM9");
                }
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
            return BadRequest("Missing found");
        }

        return BadRequest("Invalid token");
    }


}