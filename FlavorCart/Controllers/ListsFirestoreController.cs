
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class ListsFirestoreController : ControllerBase
{
    private readonly ILogger<ListsFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly ListsRepository _listsRepository = new();

    public ListsFirestoreController(ILogger<ListsFirestoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Lists>>> GetAllListsAsync()
    {
        return Ok(await _listsRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Lists>> GetListsAsync(string id)
    {
        var lists = new Lists()
        {
            Id = id
        };

        return Ok(await _listsRepository.GetAsync(lists));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdateListsAsync(string id, Lists lists)
    {
        if (id != lists.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _listsRepository.UpdateAsync(lists));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteListsAsync(string id, Lists lists)
    {
        if (id != lists.Id)
        {
            return BadRequest("Id must match.");
        }

        await _listsRepository.DeleteAsync(lists);

        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult<Lists>> AddListsAsync(Lists lists)
    {
        return Ok(await _listsRepository.AddAsync(lists));
    }
    
    [HttpGet]
    [Route("user/{user}")]
    public async Task<ActionResult<User>> GetListByUser(string user)
    {
        return Ok(await _listsRepository.GetListByUser(user));
    }
    

}