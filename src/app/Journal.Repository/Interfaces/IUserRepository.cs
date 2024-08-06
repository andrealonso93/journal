using Journal.Domain.Models;

namespace Journal.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmail(string email);
    }
}
