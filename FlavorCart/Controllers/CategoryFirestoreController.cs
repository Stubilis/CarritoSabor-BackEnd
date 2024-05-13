
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly CategoryRepository _categoryRepository = new();
    private UserTokenController _usertokenFirestoreController;

    public CategoryFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategoriesAsync()
    {
        // Before returning the data, we need to verify the token
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            return Ok(await _categoryRepository.GetAllAsync());
        }
        return BadRequest("Invalid token");
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Category>> GetCategoryAsync(string id)
    {
        // Before returning the data, we need to verify the token
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            var category = new Category()
            {
                Id = id
            };

            return Ok(await _categoryRepository.GetAsync(category));
        }
        return BadRequest("Invalid token");
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateCategoryAsync(string id, Category category)
    {
        // Before returning the data, we need to verify the token
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            if (id != category.Id)
            {
                return BadRequest("Id must match.");
            }

            return Ok(await _categoryRepository.UpdateAsync(category));
        }
        return BadRequest("Invalid token");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(string id)
    {
        // Before returning the data, we need to verify the token
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
          Category category = new Category();
            category.Id = id;
                         

            await _categoryRepository.DeleteAsync(category);

            return Ok("Deleted");
        }
        return BadRequest("Invalid token");
    }

    [HttpPost]
    public async Task<ActionResult<Category>> AddCategoryAsync(Category category)
    {
        // Before returning the data, we need to verify the token
        var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
        if (ok != null)
        {
            return Ok(await _categoryRepository.AddAsync(category));
        }
        return BadRequest("Invalid token");
    }
}
