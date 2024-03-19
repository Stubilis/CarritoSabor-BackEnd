
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryFirestoreController : ControllerBase
{
    private readonly ILogger<FirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly CategoryRepository _categoryRepository = new();

    public CategoryFirestoreController(ILogger<FirestoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllUsersAsync()
    {
        return Ok(await _categoryRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Category>> GetUserAsync(string id)
    {
        var user = new Category()
        {
            Id = id
        };

        return Ok(await _categoryRepository.GetAsync(user));
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateUserAsync(string id, Category user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _categoryRepository.UpdateAsync(user));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUserAsync(string id, Category user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id must match.");
        }

        await _categoryRepository.DeleteAsync(user);

        return Ok();
    }


    [HttpPut]
    public async Task<ActionResult<Category>> DeleteUserAsync(Category user)
    {
        return Ok(await _categoryRepository.AddAsync(user));
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