using Journal.Domain.Models;
using Journal.Domain.Services;

internal class UserService : IUserService
{
    public Task<User?> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> UpdateUserAsync(int id, User user)
    {
        throw new NotImplementedException();
    }
}

