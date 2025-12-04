using System.ComponentModel.DataAnnotations;

namespace PlaceRentalApp.Application.Models;

public class CreateCommentInputModel
{
    [Required(ErrorMessage = "IdUser is required")]
    public int IdUser { get; set; }

    [Required(ErrorMessage = "Comments is required")]
    [StringLength(2000, MinimumLength = 3, ErrorMessage = "Comments must be between 3 and 2000 characters")]
    public string Comments { get; set; }
}