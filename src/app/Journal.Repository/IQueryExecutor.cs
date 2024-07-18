namespace Journal.Repository;

public interface IQueryExecutor
{
    Task<bool> ExecuteNonQuery(string script, object parameters);
    Task<T?> Find<T>(string query, object parameters);
    Task<IEnumerable<T>> List<T>(string query, object? parameters = null);
}
