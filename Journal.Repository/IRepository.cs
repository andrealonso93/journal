namespace Journal.Repository
{
    public interface IRepository<T>
    {
        bool Insert(T insertObject);
        bool Update(T updateObject);
        bool Delete(int objecttId);
        T? Find(int objectId);
        IEnumerable<T> List();
    }
}