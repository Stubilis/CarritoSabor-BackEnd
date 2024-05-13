
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class ListsFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly ListsRepository _listsRepository = new();
    private UserTokenController _usertokenFirestoreController;
    public ListsFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
    }

    [HttpGet]
    public async Task<ActionResult<List<Lists>>> GetAllListsAsync()
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _listsRepository.GetAllAsync());
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
    public async Task<ActionResult<Lists>> GetListsAsync(string id)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                var lists = new Lists()
                {
                    Id = id
                };

                return Ok(await _listsRepository.GetAsync(lists));
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
    public async Task<ActionResult<User>> UpdateListsAsync(string id, Lists lists)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {

                if (id != lists.Id)
                {
                    return BadRequest("Id must match.");
                }

                return Ok(await _listsRepository.UpdateAsync(lists));
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
    public async Task<ActionResult> DeleteListsAsync(string id)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                if (id != lists.Id)
                {
                    return BadRequest("Id must match.");
                }

                await _listsRepository.DeleteAsync(lists);

                return Ok();
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }


    [HttpPost]
    public async Task<ActionResult<Lists>> AddListsAsync(Lists lists)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _listsRepository.AddAsync(lists));
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
    public async Task<ActionResult<Lists>> GetListsByUser(string user)
    {
        try
        {
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _listsRepository.GetListsByUser(user));
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    } 
}