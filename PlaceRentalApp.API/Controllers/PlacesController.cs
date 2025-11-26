using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlaceRentalApp.API.Models;

namespace PlaceRentalApp.API.Controllers;

[Route("api/places")]
[ApiController]
public class PlacesController : ControllerBase
{
    public PlacesController(PlacesConfiguration configuration,IOptions<PlacesConfiguration> options)
    {
        var config = options.Value;
    }

    [HttpGet]
    public IActionResult Get(string search, DateTime startDate,DateTime endDate)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreatePlaceInputModel model)
    {
        //Comentario feito para testar o tratamento global de exceções
        //throw new InvalidDataException();

        return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdatePlaceInputModel model)
    {
        return NoContent();
    }

    [HttpPost("{id}/amenities")]
    public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }

    [HttpPost("{id}/books")]
    public IActionResult PostBook(int id, CreateBookInputModel model)
    {
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateCommentInputModel model)
    {
        return NoContent();
    }

    [HttpPost("{id}/photos")]
    public IActionResult PostPlacePhoto(int id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";

        using var ms = new MemoryStream();

        file.CopyTo(ms);

        var fileBytes = ms.ToArray();
        var base64 = Convert.ToBase64String(fileBytes);

        return Ok(new { description, base64 });
    }
}
