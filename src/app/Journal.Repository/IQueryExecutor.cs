namespace Journal.Repository;

public interface IQueryExecutor
{
    bool ExecuteNonQuery(string query, object parameters);
    T? Find<T>(string query, object parameters);
    IEnumerable<T> List<T>(string query, object? parameters = null);
}
