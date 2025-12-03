using System.ComponentModel.DataAnnotations;

namespace PlaceRentalApp.API.Models;

public class CreatePlaceAmenityInputModel
{
    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 500 characters")]
    public string Description { get; set; }
}