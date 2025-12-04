using System.ComponentModel.DataAnnotations;

namespace PlaceRentalApp.Application.Models;

public class CreatePlaceInputModel
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

    [Required(ErrorMessage = "Address is required")]
    public AddressInputModel Address { get; set; }

    [Required(ErrorMessage = "AllowedNumberPerson is required")]
    [Range(1, 100, ErrorMessage = "AllowedNumberPerson must be between 1 and 100")]
    public int AllowedNumberPerson { get; set; }

    public bool AllowPets { get; set; }

    [Required(ErrorMessage = "CreatedBy is required")]
    public int CreatedBy { get; set; }
}

public class AddressInputModel
{
    [Required(ErrorMessage = "Street is required")]
    [StringLength(200, ErrorMessage = "Street must not exceed 200 characters")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Number is required")]
    [StringLength(20, ErrorMessage = "Number must not exceed 20 characters")]
    public string Number { get; set; }

    [Required(ErrorMessage = "ZipCode is required")]
    [StringLength(20, ErrorMessage = "ZipCode must not exceed 20 characters")]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "District is required")]
    [StringLength(100, ErrorMessage = "District must not exceed 100 characters")]
    public string District { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City must not exceed 100 characters")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required")]
    [StringLength(50, ErrorMessage = "State must not exceed 50 characters")]
    public string State { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(100, ErrorMessage = "Country must not exceed 100 characters")]
    public string Country { get; set; }
}