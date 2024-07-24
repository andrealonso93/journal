namespace Journal.Repository
{
    public interface IRepository<T>
    {
        Task<T?> Insert(T insertObject);
        Task<T> Update(T updateObject);
        Task<bool> Delete(int objectId);
        Task<T?> Find(int objectId);
        Task<IEnumerable<T>> List();
    }
}