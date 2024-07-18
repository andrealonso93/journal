using System.Text;
using Journal.Database;
using Journal.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Journal.Repository;

/// <summary>
/// Repository to mange database operations of journal inputs
/// </summary>
public class InputRepository : IRepository<Input>
{
    private readonly IQueryExecutor _queryExecutor;
    private readonly InputContext _inputDbContext;
    private readonly ILogger<InputRepository> _logger;

    public InputRepository(IQueryExecutor queryExecutor, ILogger<InputRepository> logger, InputContext inputDbContext)
    {
        _queryExecutor = queryExecutor;
        _logger = logger;
        _inputDbContext = inputDbContext;
    }

    public async Task<bool> Delete(int objectId)
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
        return await _queryExecutor.ExecuteNonQuery(query.ToString(), parameters);
    }

    public async Task<Input?> Find(int objectId)
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
        return await _queryExecutor.Find<Input>(query.ToString(), parameters);
    }

    public async Task<Input?> Insert(Input insertObject)
    {
        _logger.LogInformation($"Trying to insert new Input. {JsonConvert.SerializeObject(insertObject)}");
        var addedObject = await _inputDbContext.Inputs.AddAsync(insertObject);

        if (addedObject.State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            await _inputDbContext.SaveChangesAsync();
            return addedObject.Entity;
        }

        return null;
    }

    public async Task<IEnumerable<Input>> List()
    {
        var query = new StringBuilder();
        query.AppendLine(@"
            SELECT
                *
            FROM
                INPUTS
        ");

        _logger.LogInformation($"Trying to list all Inputs");
        return await _queryExecutor.List<Input>(query.ToString());
    }

    public async Task<bool> Update(Input updateObject)
    {
        var query = new StringBuilder();

        query.AppendLine(@"
            UDATE INPUTS SET
                InputText = @InputText,
                UpdateDateTime = @UpdateDateTime
            WHERE ID = @Id
        ");

        _logger.LogInformation($"Trying to insert new Input. {JsonConvert.SerializeObject(updateObject)}");
        return await _queryExecutor.ExecuteNonQuery(query.ToString(), updateObject);
    }
}
