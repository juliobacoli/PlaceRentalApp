using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Application.Services;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Infrastructure.Persistence;

namespace PlaceRentalApp.API.Controllers;

[Route("api/places")]
[ApiController]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _placeService;
    public PlacesController(IPlaceService placeService)
    {
        _placeService = placeService;
    }

    [HttpGet]
    public IActionResult Get(string? search, DateTime startDate, DateTime endDate)
    {
        var availablePlaces = _placeService.GetAllAvailable(search, startDate, endDate);

        return Ok(availablePlaces);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var place = _placeService.GetById(id);

        return Ok(place);
    }

    [HttpPost]
    public IActionResult Post(CreatePlaceInputModel model)
    {
        var id = _placeService.Insert(model);
        return CreatedAtAction(nameof(GetById), new { id }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdatePlaceInputModel model)
    {
        _placeService.Update(id, model);

        return NoContent();
    }

    [HttpPost("{id}/amenities")]
    public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        _placeService.InsertAmenity(id,model);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _placeService.Detele(id);

        return NoContent();
    }

    [HttpPost("{id}/books")]
    public IActionResult PostBook(int id, CreateBookInputModel model)
    {
        _placeService.Book(id, model);

        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateCommentInputModel model)
    {
        _placeService.AddComment(id, model);

        return NoContent();
    }

    //[HttpPost("{id}/photos")]
    //public IActionResult PostPlacePhoto(int id, IFormFile file)
    //{
    //    var exists = _context.Places.Any(p => p.Id == id && !p.IsDeleted);

    //    if (!exists)
    //        return NotFound();

    //    var description = $"File: {file.FileName}, Size: {file.Length}";

    //    using var ms = new MemoryStream();

    //    file.CopyTo(ms);

    //    var fileBytes = ms.ToArray();
    //    var base64 = Convert.ToBase64String(fileBytes);

    //    return Ok(new { description, base64 });
    //}
}
