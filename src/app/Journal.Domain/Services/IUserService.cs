using Journal.Domain.Models;

namespace Journal.Domain.Services
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
