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
        var result = _placeService.GetAllAvailable(search, startDate, endDate);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _placeService.GetById(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(CreatePlaceInputModel model)
    {
        var result = _placeService.Insert(model);

        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdatePlaceInputModel model)
    {
        var result = _placeService.Update(id, model);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    [HttpPost("{id}/amenities")]
    public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        var result = _placeService.InsertAmenity(id, model);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _placeService.Detele(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    [HttpPost("{id}/books")]
    public IActionResult PostBook(int id, CreateBookInputModel model)
    {
        var result = _placeService.Book(id, model);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateCommentInputModel model)
    {
        var result = _placeService.AddComment(id, model);

        if (!result.IsSuccess)
            return NotFound(result);

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
