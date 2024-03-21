
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

    public CategoryFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategoriesAsync()
    {
        return Ok(await _categoryRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Category>> GetCategoryAsync(string id)
    {
        var category = new Category()
        {
            Id = id
        };

        return Ok(await _categoryRepository.GetAsync(category));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateCategoryAsync(string id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _categoryRepository.UpdateAsync(category));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(string id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest("Id must match.");
        }

        await _categoryRepository.DeleteAsync(category);

        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult<Category>> AddCategoryAsync(Category category)
    {
        return Ok(await _categoryRepository.AddAsync(category));
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