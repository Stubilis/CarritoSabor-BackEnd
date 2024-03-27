
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class PriceFirestoreController : ControllerBase
{
    private readonly ILogger<UserFirestoreController> _logger;
    // This should be injected - This is only an example
    private readonly PriceRepository _priceRepository = new();

    public PriceFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Price>>> GetAllPricesAsync()
    {
        return Ok(await _priceRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Price>> GetPriceAsync(string id)
    {
        var price = new Price()
        {
            Id = id
        };

        return Ok(await _priceRepository.GetAsync(price));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<User>> UpdatePriceAsync(string id, Price price)
    {
        if (id != price.Id)
        {
            return BadRequest("Id must match.");
        }

        return Ok(await _priceRepository.UpdateAsync(price));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeletePriceAsync(string id, Price price)
    {
        if (id != price.Id)
        {
            return BadRequest("Id must match.");
        }

        await _priceRepository.DeleteAsync(price);

        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult<Price>> AddPriceAsync(Price price)
    {
        return Ok(await _priceRepository.AddAsync(price));
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