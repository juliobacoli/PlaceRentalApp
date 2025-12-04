using System.ComponentModel.DataAnnotations;

namespace PlaceRentalApp.Application.Models;

public class CreateBookInputModel
{
    [Required(ErrorMessage = "IdUser is required")]
    public int IdUser { get; set; }

    [Required(ErrorMessage = "IdPlace is required")]
    public int IdPlace { get; set; }

    [Required(ErrorMessage = "StartDate is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "EndDate is required")]
    public DateTime EndDate { get; set; }

    [StringLength(1000, ErrorMessage = "Comments must not exceed 1000 characters")]
    public string? Comments { get; set; }
}