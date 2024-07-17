using Journal.Domain;

namespace Journal.Service
{
    public interface IInputService
    {
        Task<IEnumerable<Input>> GetAllInputs();
        Task<Input?> CreateInput(string entryText);
    }
}
