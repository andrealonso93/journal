using Journal.Database;
using Journal.Domain;
using Journal.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Journal.Repository.Implementations
{
    internal class UserRepository : IUserRepository
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly JournalContext _journalDbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IQueryExecutor queryExecutor, JournalContext journalContext, ILogger<UserRepository> logger)
        {
            _queryExecutor = queryExecutor;
            _journalDbContext = journalContext;
            _logger = logger;
        }


        #region Write Operations

        public async Task<bool> Delete(int objectId)
        {
            var userInputs = _journalDbContext.Inputs.Where(i => i.UserId == objectId);
            try
            {
                if (userInputs.Any())
                    _journalDbContext.Inputs.RemoveRange(userInputs);

                _journalDbContext.Users.Remove(new User { Id = objectId });
                await _journalDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                _logger.LogError("Unable to delete user with id {ObjectId}", objectId);
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

        public Task<User?> Find(int objectId)
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email)
        {
            throw new NotImplementedException();
        }



        public Task<IEnumerable<User>> List()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
