using Journal.Domain;
using Microsoft.Extensions.Configuration;

namespace Journal.Repository;

/// <summary>
/// Repository to mange database operations of journal inputs
/// </summary>
public class InputRepository : BaseRepository, IRepository<Input>
{
    public InputRepository(IConfiguration config) : base(config) { }

    public bool Delete(int objectId)
    {
        throw new NotImplementedException();
    }

    public Input Find(int objecId)
    {
        throw new NotImplementedException();
    }

    public bool Insert(Input insertObject)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Input> List()
    {
        throw new NotImplementedException();
    }

    public bool Update(Input updateObject)
    {
        throw new NotImplementedException();
    }
}
