using Journal.Domain;

namespace Journal.Service
{
    public interface IInputService
    {
        Task<IEnumerable<Input>> GetAllInputs();
    }
}
