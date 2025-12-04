using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;

namespace PlaceRentalApp.Application.Services;

public interface IPlaceService
{
    void AddComment(int id, CreateCommentInputModel model);
    void Book(int id, CreateBookInputModel model);
    List<Place> GetAllAvailable(string search, DateTime? startDate, DateTime? endDate);
    Place? GetById(int id);
    int Insert(CreatePlaceInputModel model);
    void InsertAmenity(int id, CreatePlaceAmenityInputModel model);
    void Update(int id, UpdatePlaceInputModel model);
    void Detele(int id);
}
