namespace Journal.Repository
{
    public interface IRepository<T>
    {
        bool Insert(T insertObject);
        bool Update(T updateObject);
        bool Delete(int objectId);
        T Find(int objecId);
        IEnumerable<T> List();
    }
}