using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;

namespace PlaceRentalApp.Application.Services;

public interface IPlaceService
{
    ResultViewModel AddComment(int id, CreateCommentInputModel model);
    ResultViewModel Book(int id, CreateBookInputModel model);
    ResultViewModel<List<PlaceViewModel>> GetAllAvailable(string search, DateTime? startDate, DateTime? endDate);
    ResultViewModel<PlaceDetailsViewModel?> GetById(int id);
    ResultViewModel<int> Insert(CreatePlaceInputModel model);
    ResultViewModel InsertAmenity(int id, CreatePlaceAmenityInputModel model);
    ResultViewModel Update(int id, UpdatePlaceInputModel model);
    ResultViewModel Detele(int id);
}
