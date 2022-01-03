using System.Text;
using Journal.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Journal.Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        public UserRepository(IConfiguration config, ILogger logger) : base(config, logger) { }

        public bool Delete(int objectId)
        {
            var query = new StringBuilder();

            query.AppendLine(@"
                DELETE FROM USERS WHERE ID = @Id
            ");

            var parameters = new
            {
                @Id = objectId
            };

            logger.LogInformation($"Trying to delete User with Id: {objectId}");
            return ExecuteNonQuery(query.ToString(), parameters);
        }

        public User? Find(int objectId)
        {
            var query = new StringBuilder();
            query.AppendLine(@"
                SELECT * FROM USERS WHERE ID = @Id
            ");

            logger.LogInformation($"Trying to find User by Id: {objectId}");
            return Find<User>(query.ToString(), objectId);
        }

        public bool Insert(User insertObject)
        {
            var query = new StringBuilder();

            query.AppendLine(@"
                INSERT INTO USERS(Name, CreationDateTime) VALUES (@Name, CreationDate)
            ");

            logger.LogInformation($"Trying to delete Input with ID: {JsonConvert.SerializeObject(insertObject)}");
            return ExecuteNonQuery(query.ToString(), insertObject);
        }

        public IEnumerable<User> List()
        {
            var query = new StringBuilder();
            query.AppendLine(@"
                SELECT * FROM USERS
            ");

            logger.LogInformation("Trying to list all Users.");
            return List<User>(query.ToString());
        }

        public bool Update(User updateObject)
        {
            var query = new StringBuilder();

            query.AppendLine(@"
                UPDATE USERS SET
                    Name = @Name,
                    UpdateDateTime = @UpdateDateTime
                WHERE
                    ID = @Id
            ");

            logger.LogInformation($"Trying to update Input with ID: {JsonConvert.SerializeObject(updateObject)}");
            return ExecuteNonQuery(query.ToString(), updateObject);
        }
    }
}