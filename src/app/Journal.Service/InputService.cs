using Journal.Domain;
using Journal.Notification;
using Journal.Repository;
using Microsoft.Extensions.Logging;

namespace Journal.Service;
public class InputService : IInputService
{
    private readonly IRepository<Input> _inputRepository;
    private readonly ILogger<InputService> _logger;
    private readonly NotificationContext _notificationContext;

    public InputService(IRepository<Input> inputRepository, ILogger<InputService> logger, NotificationContext notificationContext)
    {
        _inputRepository = inputRepository;
        _logger = logger;
        _notificationContext = notificationContext;
    }

    public async Task<Input?> CreateInput(string entryText)
    {
        if(entryText.Length > 500)
        {
            _logger.LogError("Input text is too long.");
            _notificationContext.AddNotification(400, "Input text is too long.");
            return null;
        }

        _logger.LogInformation("Trying to create new input");
        var input = Input.BuildNewInput(entryText);
        var newInput = await _inputRepository.Insert(input);
        
        if(newInput == null)
        {
            _logger.LogError("Failed to create new input");
            _notificationContext.AddNotification(400, "Failed to create new input");
        }

        return newInput;
    }

    public async Task<IEnumerable<Input>> GetAllInputs()
    {
        _logger.LogInformation("Trying to get all inputs");
        return await _inputRepository.List();
    }
}
