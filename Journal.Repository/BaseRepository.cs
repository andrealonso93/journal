using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Journal.Repository;

public class BaseRepository
{
    protected readonly IDbConnection connection;
    protected readonly ILogger logger;

    public BaseRepository(IConfiguration config, ILogger logger)
    {
        var connectionString = config.GetConnectionString("SQL_Connection");
        this.connection = new SqlConnection(connectionString);

        this.logger = logger;
    }

    public bool ExecuteNonQuery(string query, object parameters)
    {
        try
        {
            connection.Execute(query, parameters);

            return true;
        }
        catch (DbException dbException)
        {
            logger.LogError(dbException, "Error executing non query db script.", query);
            return false;
        }
    }

    public T? Find<T>(string query, object parameters)
    {
        try
        {
            return connection.Query<T>(query, parameters).FirstOrDefault();
        }
        catch (DbException dbException)
        {
            logger.LogError(dbException, "Error executing find query.", query);
            return default;
        }
    }

    public IEnumerable<T> List<T>(string query, object? parameters = null)
    {
        try
        {
            return connection.Query<T>(query, parameters);
        }
        catch (DbException dbException)
        {
            logger.LogError(dbException, "Error executing list query.", query);
            return new List<T>();
        }
    }
}