using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Journal.Repository;

public class SqlQueryExecutor : IQueryExecutor
{
    protected readonly IDbConnection _connection;
    protected readonly ILogger<SqlQueryExecutor> _logger;

    public SqlQueryExecutor(IConfiguration config, ILogger<SqlQueryExecutor> logger)
    {
        var connectionString = config.GetConnectionString("journal");
        _connection = new SqlConnection(connectionString);
        _logger = logger;
    }

    public async Task<bool> ExecuteNonQuery(string query, object parameters)
    {
        try
        {
            var executionResponse = await _connection.ExecuteAsync(query, parameters);
            return executionResponse > 0;
        }
        catch (DbException dbException)
        {
            _logger.LogError(dbException, "Error executing non query db script: {Query}", query);
            return false;
        }
    }

    public async Task<T?> Find<T>(string query, object parameters)
    {
        try
        {
            var objects = await _connection.QueryAsync<T>(query, parameters);
            return objects.FirstOrDefault();
        }
        catch (DbException dbException)
        {
            _logger.LogError(dbException, "Error executing find query: {Query}", query);
            return default;
        }
    }

    public async Task<IEnumerable<T>> List<T>(string query, object? parameters = null)
    {
        try
        {
            return await _connection.QueryAsync<T>(query, parameters);
        }
        catch (DbException dbException)
        {
            _logger.LogError(dbException, "Error executing list query: {Query}", query);
            return new List<T>();
        }
    }
}