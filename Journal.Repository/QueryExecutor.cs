using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Journal.Repository;

public class QueryExecutor : IQueryExecutor
{
    protected readonly IDbConnection _connection;
    protected readonly ILogger _logger;

    public QueryExecutor(IConfiguration config, ILogger logger)
    {
        var connectionString = config.GetConnectionString("SQL_Connection");
        _connection = new SqlConnection(connectionString);
        _logger = logger;
    }

    public bool ExecuteNonQuery(string query, object parameters)
    {
        try
        {
            _connection.Execute(query, parameters);
            return true;
        }
        catch (DbException dbException)
        {
            _logger.LogError(dbException, "Error executing non query db script: {Query}", query);
            return false;
        }
    }

    public T? Find<T>(string query, object parameters)
    {
        try
        {
            return _connection.Query<T>(query, parameters).FirstOrDefault();
        }
        catch (DbException dbException)
        {
            _logger.LogError(dbException, "Error executing find query: {Query}", query);
            return default;
        }
    }

    public IEnumerable<T> List(string query, object? parameters = null)
    {
        try
        {
            return _connection.Query<T>(query, parameters);
        }
        catch (DbException dbException)
        {
            _logger.LogError(dbException, "Error executing list query: {Query}", query);
            return new List<T>();
        }
    }
}