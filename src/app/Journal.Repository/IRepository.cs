namespace Journal.Repository
{
    public interface IRepository<T>
    {
        Task<T> Insert(T insertObject);
        Task<bool> Update(T updateObject);
        Task<bool> Delete(int objectId);
        Task<T?> Find(int objecId);
        Task<IEnumerable<T>> List();
    }
}