using Journal.Domain;
using Journal.Repository;
using Microsoft.Extensions.Logging;

namespace Journal.Service;
public class InputService : IInputService
{
    private readonly IRepository<Input> _inputRepository;
    private readonly ILogger<InputService> _logger;

    public InputService(IRepository<Input> inputRepository, ILogger<InputService> logger)
    {
        _inputRepository = inputRepository;
        _logger = logger;
    }

    public async Task<Input?> CreateInput(string entryText)
    {
        _logger.LogInformation("Trying to create new input");
        var input = Input.BuildNewInput(entryText);
        var newInputId = await _inputRepository.Insert(input);
        if (newInputId > 0)
            return await _inputRepository.Find(newInputId);

        return null;
    }

    public async Task<IEnumerable<Input>> GetAllInputs()
    {
        _logger.LogInformation("Trying to get all inputs");
        return await _inputRepository.List();
    }
}
