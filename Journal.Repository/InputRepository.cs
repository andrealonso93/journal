using System.Data.Common;
using System.Text;
using Journal.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Journal.Repository;

/// <summary>
/// Repository to mange database operations of journal inputs
/// </summary>
public class InputRepository : SqlQueryExecutor, IRepository<Input>
{
    public InputRepository(IConfiguration config, ILogger logger) : base(config, logger) { }

    public bool Delete(int objectId)
    {
        var query = new StringBuilder();

        query.AppendLine(@"
            DELETE FROM INPUTS WHERE ID = @Id
        ");

        var parameters = new
        {
            @Id = objectId
        };

        _logger.LogInformation($"Trying to delete Input with ID: {objectId}");
        return ExecuteNonQuery(query.ToString(), parameters);
    }

    public Input? Find(int objectId)
    {
        var query = new StringBuilder();
        query.AppendLine(@"
            SELECT
                *
            FROM
                INPUTS
            WHERE ID = @Id
        ");

        var parameters = new
        {
            @Id = objectId
        };

        _logger.LogInformation($"Trying to find Input by ID: {objectId}");
        return Find<Input>(query.ToString(), parameters);
    }

    public bool Insert(Input insertObject)
    {
        var query = new StringBuilder();

        query.AppendLine(@"
            DELETE FROM INPUTS WHERE ID = @Id
        ");

        _logger.LogInformation($"Trying to insert new Input. {JsonConvert.SerializeObject(insertObject)}");
        return ExecuteNonQuery(query.ToString(), insertObject);
    }

    public IEnumerable<Input> List()
    {
        var query = new StringBuilder();
        query.AppendLine(@"
            SELECT
                *
            FROM
                INPUTS
        ");

        _logger.LogInformation($"Trying to list all Inputs");
        return List<Input>(query.ToString());
    }

    public bool Update(Input updateObject)
    {
        var query = new StringBuilder();

        query.AppendLine(@"
            UDATE INPUTS SET
                InputText = @InputText,
                UpdateDateTime = @UpdateDateTime
            WHERE ID = @Id
        ");

        _logger.LogInformation($"Trying to insert new Input. {JsonConvert.SerializeObject(updateObject)}");
        return ExecuteNonQuery(query.ToString(), updateObject);
    }
}
