using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.API.Models;
using PlaceRentalApp.API.Persistance;

namespace PlaceRentalApp.API.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly PlaceRentalDbContext _context;
    public UsersController(PlaceRentalDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id && !u.IsDeleted);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var user = new Entities.User(
            model.FullName,
            model.Email,
            model.BirthDate
        );

        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, model);
    }
}
