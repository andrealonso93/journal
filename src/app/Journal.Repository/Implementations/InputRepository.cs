using Journal.Database;
using Journal.Domain;
using Journal.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Journal.Repository.Implementations;

/// <summary>
/// Repository to mange database operations of journal inputs
/// </summary>
public class InputRepository : IRepository<Input>
{
    private readonly IQueryExecutor _queryExecutor;
    private readonly JournalContext _journalDbContext;
    private readonly ILogger<InputRepository> _logger;

    public InputRepository(IQueryExecutor queryExecutor, ILogger<InputRepository> logger, JournalContext journalDbContext)
    {
        _queryExecutor = queryExecutor;
        _logger = logger;
        _journalDbContext = journalDbContext;
    }

    #region Read Operations

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

        _logger.LogInformation("Trying to find Input by ID: {ObjectId}", objectId);
        return await _queryExecutor.Find<Input>(query.ToString(), parameters);
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

    #endregion

    #region Write Operations
    public async Task<bool> Delete(int objectId)
    {
        _logger.LogInformation("Trying to delete Input with ID: {ObjectId}", objectId);
        try
        {
            _journalDbContext.Inputs.Remove(new Input { Id = objectId });
            await _journalDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error deleting Input with ID: {ObjectId}", objectId);
            _logger.LogError(ex, "Exception:");
            return false;
        }
    }

    public async Task<Input?> Insert(Input insertObject)
    {
        _logger.LogInformation("Trying to insert new Input. {JsonObject}", JsonConvert.SerializeObject(insertObject));
        var addedObject = await _journalDbContext.Inputs.AddAsync(insertObject);

        if (addedObject.State == EntityState.Added)
        {
            await _journalDbContext.SaveChangesAsync();
            return addedObject.Entity;
        }

        return null;
    }

    public async Task<Input> Update(Input updateObject)
    {
        _logger.LogInformation("Trying to update Input with id: {Id}", updateObject.Id);
        var updatedOject = _journalDbContext.Inputs.Update(updateObject);

        if (updatedOject.State == EntityState.Modified)
        {
            await _journalDbContext.SaveChangesAsync();
            return updatedOject.Entity;
        }

        return updateObject;
    }

    #endregion
}
