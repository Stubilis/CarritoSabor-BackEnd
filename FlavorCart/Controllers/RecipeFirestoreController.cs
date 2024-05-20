
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class RecipeFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly RecipeRepository _recipeRepository = new();
    private UserTokenController _usertokenFirestoreController;
    public RecipeFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetAllRecipeAsync()
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _recipeRepository.GetAllAsync());
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing found");
        }
        return BadRequest("Invalid token");
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipeAsync(string id)
    {

        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                var recipe = new Recipe()
                {
                    Id = id
                };

                return Ok(await _recipeRepository.GetAsync(recipe));
            }

        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Ids must match");
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateRecipeAsync(string id, Recipe recipe)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {

                if (id != recipe.Id)
                {
                    return BadRequest("Id must match.");
                }

                return Ok(await _recipeRepository.UpdateAsync(recipe));
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
    public async Task<ActionResult> DeleteRecipeAsync(string id)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
               Recipe recipe = new Recipe()
               {
                    Id = id
                };

                await _recipeRepository.DeleteAsync(recipe);

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
    public async Task<ActionResult<Recipe>> AddRecipeAsync(Recipe recipe)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                
                return Ok(await _recipeRepository.AddAsync(recipe));
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }

    [HttpGet]
    [Route("user/{user}")]
    public async Task<ActionResult<Recipe>> GetRecipeByUser(string user)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _recipeRepository.GetRecipeByUser(user));
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    } 
}