using PlaceRentalApp.Application.Exceptions;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Infrastructure.Persistence;

namespace PlaceRentalApp.Application.Services;

public interface IUserService
{
    ResultViewModel<User?> GetById(int id);
    ResultViewModel<int> Insert(CreateUserInputModel model);
}

public class UserService : IUserService
{
    private readonly PlaceRentalDbContext _context;
    public UserService(PlaceRentalDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<User?> GetById(int id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id && !u.IsDeleted);

        if (user == null)
            return ResultViewModel<User?>.Error("Usuário não encontrado");

        return ResultViewModel<User?>.Success(user);
    }

    public ResultViewModel<int> Insert(CreateUserInputModel model)
    {
        var user = new User(
                   model.FullName,
                   model.Email,
                   model.BirthDate
               );

        _context.Users.Add(user);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(user.Id);
    }
}
