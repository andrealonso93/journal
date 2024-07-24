using Journal.Domain;

namespace Journal.Service
{
    public interface IInputService
    {
        Task<Input?> GetInputAsync(int id);
        Task<IEnumerable<Input>> GetAllInputsAsync();
        Task<Input?> CreateInputAsync(string entryText);
        Task<Input?> UpdateInputAsync(int id, string entryText);
    }
}
