using Journal.Database;
using Journal.Domain.Models;
using Journal.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Journal.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly JournalContext _journalDbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(JournalContext journalContext, ILogger<UserRepository> logger, IQueryExecutor queryExecutor)
        {
            _journalDbContext = journalContext;
            _logger = logger;
            _queryExecutor = queryExecutor;
        }


        #region Write Operations

        public async Task<bool> Delete(int objectId)
        {
            var userInputs = _journalDbContext.Inputs.Where(i => i.UserId == objectId);
            try
            {
                if (await userInputs.AnyAsync())
                    _journalDbContext.Inputs.RemoveRange(userInputs);

                _journalDbContext.Users.Remove(new User { Id = objectId });

                await _journalDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to delete user with id {ObjectId}", objectId);
                _logger.LogError(ex, "Exception: ");
                return false;
            }
        }

        public async Task<User?> Insert(User insertObject)
        {
            _logger.LogInformation("Trying to insert new User. {JsonObject}", JsonConvert.SerializeObject(insertObject));
            var addedObject = await _journalDbContext.Users.AddAsync(insertObject);

            if (addedObject.State == EntityState.Added)
            {
                await _journalDbContext.SaveChangesAsync();
                return addedObject.Entity;
            }

            return null;
        }

        public Task<User> Update(User updateObject)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Read Operations

        public async Task<User?> Find(int objectId)
        {
            var sql = @"SELECT * FROM Users WHERE Id = @objectId";

            _logger.LogInformation("Trying to find user with id = {ObjectId}", objectId);
            return await _queryExecutor.Find<User>(sql, new { objectId });
        }

        public async Task<User?> FindByEmail(string email)
        {
            var sql = @"SELECT TOP 1 * FROM Users WHERE Email = @email";

            _logger.LogInformation("Trying to find user with email = {Email}", email);
            return await _queryExecutor.Find<User>(sql, new { email });
        }

        public async Task<IEnumerable<User>> List()
        {
            var sql = @"SELECT * FROM Users";

            _logger.LogInformation("Trying to list all users");
            return await _queryExecutor.List<User>(sql);
        }

        #endregion
    }
}
