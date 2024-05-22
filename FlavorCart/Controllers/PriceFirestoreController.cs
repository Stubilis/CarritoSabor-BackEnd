
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class PriceFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
    
    private readonly PriceRepository _priceRepository = new();
    private UserTokenController _usertokenFirestoreController;

    public PriceFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
    }

    [HttpGet]
    public async Task<ActionResult<List<Price>>> GetAllPricesAsync()
    {
        try
        {
            //Before returning the data, we need to verify the token
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _priceRepository.GetAllAsync());
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Price>> GetPriceAsync(string id)
    {
        try
        {
            
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                var price = new Price()
        {
            Id = id
        };

        return Ok(await _priceRepository.GetAsync(price));
            }
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdatePriceAsync(string id, Price price)
    {
        try
        {

            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                if (id != price.Id)
        {
            return BadRequest("Id must match.");
        }
            return Ok(await _priceRepository.UpdateAsync(price));
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
    public async Task<ActionResult> DeletePriceAsync(string id)
    {
        try
        {

            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                Price price = new Price()
                {
                    Id = id
                };
                //Get the complete data from the price so we can update the articleId
                price = await _priceRepository.GetAsync(price);
                await _priceRepository.DeleteAsync(price);
               
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
    public async Task<ActionResult<Price>> AddPriceAsync(Price price)
    {
        try
        {

            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                price.setPriceDate();
        await _priceRepository.AddAsync(price);
       
        return Ok(price);
}
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }

    //GET prices by article
    [HttpGet]
    [Route("article/{article}")]
    public async Task<ActionResult<Price>> GetPriceByArticle(string article)
    {
        try
        {

            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)
            {
                return Ok(await _priceRepository.GetPriceByArticle(article));
}
        }
        catch (Exception e)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("Invalid token");
    }
  

}