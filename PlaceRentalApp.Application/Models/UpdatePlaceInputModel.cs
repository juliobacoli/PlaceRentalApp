using System.ComponentModel.DataAnnotations;

namespace PlaceRentalApp.Application.Models;

public class UpdatePlaceInputModel
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "DailyPrice is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "DailyPrice must be greater than 0")]
    public decimal DailyPrice { get; set; }
}
