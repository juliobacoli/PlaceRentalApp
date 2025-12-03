using System.ComponentModel.DataAnnotations;

namespace PlaceRentalApp.API.Models;

public class CreateUserInputModel
{
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "FullName must be between 3 and 200 characters")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "BirthDate is required")]
    public DateTime BirthDate { get; set; }
}