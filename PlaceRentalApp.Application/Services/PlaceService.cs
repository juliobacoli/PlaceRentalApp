using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Core.ValueObjects;
using PlaceRentalApp.Infrastructure.Persistence;

namespace PlaceRentalApp.Application.Services;

public class PlaceService : IPlaceService
{
    private readonly PlaceRentalDbContext _context;
    public PlaceService(PlaceRentalDbContext context)
    {
        _context = context;
    }
    public ResultViewModel Book(int id, CreateBookInputModel model)
    {
        var exists = _context.Places.Any(p => p.Id == id && !p.IsDeleted);

        if (!exists)
            return ResultViewModel.Error("Place não encontrado");

        var user = _context.Users.Any(u => u.Id == model.IdUser && !u.IsDeleted);

        if (!user)
            return ResultViewModel.Error("Usuário não encontrado");

        var book = new PlaceBook
        (
            model.IdUser,
            id,
            model.StartDate,
            model.EndDate,
            model.Comments
        );

        _context.PlaceBooks.Add(book);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel Detele(int id)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return ResultViewModel.Error("Place não encontrado");

        place.SetAsDeleted();

        _context.Places.Update(place);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel<List<PlaceViewModel>> GetAllAvailable(string search, DateTime? startDate, DateTime? endDate)
    {
        var availablePlaces = _context
            .Places
            .Include(p => p.User)
            .Where(p =>
                (string.IsNullOrEmpty(search) || p.Title.Contains(search)) &&
                !p.Books.Any(b =>
                    (startDate >= b.StartDate && startDate <= b.EndDate) ||
                    (endDate >= b.StartDate && endDate <= b.EndDate) ||
                    (startDate <= b.StartDate && endDate >= b.EndDate)) &&
                !p.IsDeleted).ToList();

        var model = availablePlaces
            .Select(PlaceViewModel.FromEntity)
            .ToList();

        return ResultViewModel<List<PlaceViewModel>>.Success(model);
    }

    public ResultViewModel<PlaceDetailsViewModel?> GetById(int id)
    {
        var place = _context.Places
                .Include(p => p.Books)
                .Include(p => p.Amenities)
                .Include(p => p.Comments)
                .Include(p => p.User)
                .SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return ResultViewModel<PlaceDetailsViewModel?>.Error("Place não encontrado");

        return ResultViewModel<PlaceDetailsViewModel?>.Success(PlaceDetailsViewModel.FromEntity(place));
    }

    public ResultViewModel<int> Insert(CreatePlaceInputModel model)
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

        return ResultViewModel<int>.Success(place.Id);
    }

    public ResultViewModel InsertAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        var exists = _context.Places.Any(p => p.Id == id && !p.IsDeleted);

        if (!exists)
            return ResultViewModel.Error("Place não encontrado");

        var amenity = new PlaceAmenity(id, model.Description);

        _context.PlaceAmenities.Add(amenity);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel Update(int id, UpdatePlaceInputModel model)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return ResultViewModel.Error("Place não encontrado");

        place.Update(model.Title, model.Description, model.DailyPrice);
        _context.Places.Update(place);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel AddComment(int id, CreateCommentInputModel model)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id && !p.IsDeleted);

        if (place == null)
            return ResultViewModel.Error("Place não encontrado");

        var user = _context.Users.SingleOrDefault(u => u.Id == model.IdUser);

        if (user == null)
            return ResultViewModel.Error("Usuário não encontrado");

        var comment = new PlaceComment(model.IdUser, id, model.Comments);

        _context.Comments.Add(comment);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }
}
