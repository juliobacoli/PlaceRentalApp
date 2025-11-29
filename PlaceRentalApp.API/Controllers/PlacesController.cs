using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.API.Entities;
using PlaceRentalApp.API.Models;
using PlaceRentalApp.API.Persistance;
using PlaceRentalApp.API.ValueObjects;

namespace PlaceRentalApp.API.Controllers;

[Route("api/places")]
[ApiController]
public class PlacesController : ControllerBase
{
    private readonly PlaceRentalDbContext _context;
    public PlacesController(PlaceRentalDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult Get(string search, DateTime startDate, DateTime endDate)
    {
        var availablePlaces = _context
            .Places
            .Where(p =>
                p.Title.Contains(search) &&
                !p.Books.Any(b =>
                    (startDate >= b.StartDate && startDate <= b.EndDate) ||
                    (endDate >= b.StartDate && endDate <= b.EndDate)     ||
                    (startDate <= b.StartDate && endDate >= b.EndDate))  &&
                !p.IsDeleted);

        return Ok(availablePlaces);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return NotFound();

        return Ok(place);
    }

    [HttpPost]
    public IActionResult Post(CreatePlaceInputModel model)
    {
        var adress = new Address
        (
            model.Address.Street,
            model.Address.Number,
            model.Address.ZipCode,
            model.Address.District,
            model.Address.City,
            model.Address.State,
            model.Address.Country
        );

        var place = new Place
        (
            model.Title,
            model.Description,
            model.DailyPrice,
            adress,
            model.AllowedNumberPerson,
            model.AllowPets,
            model.CreatedBy
            );

        _context.Places.Add(place);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = place.Id }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdatePlaceInputModel model)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return NotFound();

        //place.Update(model.Title, model.Description, model.DailyPrice);
        _context.Places.Update(place);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/amenities")]
    public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        var exists = _context.Places.Any(p => p.Id == id && !p.IsDeleted);

        if (!exists)
            return NotFound();

        var amenity = new PlaceAmenity(id, model.Description);

        _context.PlaceAmenities.Add(amenity);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return NotFound();

        //place.SetAsDeleted();

        _context.Places.Update(place);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/books")]
    public IActionResult PostBook(int id, CreateBookInputModel model)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return NotFound();

        var book = new PlaceBook
        (
            id,
            model.IdUser,
            model.StartDate,
            model.EndDate,
            model.Comments
        );
        
        _context.PlaceBooks.Add(book);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateCommentInputModel model)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);
        if (place == null)
            return NotFound();

        var user = _context.Users.SingleOrDefault(u => u.Id == model.IdUser);
        if (user == null)
            return BadRequest("User not found.");

        var comment = new PlaceComment(model.IdUser, id, model.Comments);

        _context.Comments.Add(comment);
        _context.SaveChanges();

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
