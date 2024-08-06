using Journal.Domain.Models;
using Journal.Notification;
using Journal.Repository.Interfaces;
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

    public async Task<Input?> CreateInputAsync(string entryText)
    {
        if (entryText.Length > 500)
        {
            _logger.LogError(ConstantTexts.TextTooLong);
            _notificationContext.AddNotification(400, ConstantTexts.TextTooLong);
            return null;
        }

        _logger.LogInformation("Trying to create new input");
        var input = Input.BuildNewInput(entryText);
        var newInput = await _inputRepository.Insert(input);

        if (newInput == null)
        {
            _logger.LogError(ConstantTexts.CreateInputFail);
            _notificationContext.AddNotification(400, ConstantTexts.CreateInputFail);
        }

        return newInput;
    }

    public async Task<IEnumerable<Input>> GetAllInputsAsync()
    {
        _logger.LogInformation("Trying to get all inputs");
        return await _inputRepository.List();
    }

    public async Task<Input?> UpdateInputAsync(int id, string entryText)
    {
        var existingInput = await GetInputAsync(id);

        if (existingInput is null)
        {
            _logger.LogError(ConstantTexts.InputNotFound);
            _notificationContext.AddNotification(400, ConstantTexts.InputNotFound);
            return null;
        }

        if (entryText.Length > 500)
        {
            _logger.LogError(ConstantTexts.TextTooLong);
            _notificationContext.AddNotification(400, ConstantTexts.TextTooLong);
            return null;
        }

        _logger.LogInformation("Trying to update input");
        existingInput.InputText = entryText;
        existingInput.UpdateDateTime = DateTime.Now;
        var updatedInput = await _inputRepository.Update(existingInput);

        if (updatedInput == null)
        {
            _logger.LogError(ConstantTexts.UpdateInputFail);
            _notificationContext.AddNotification(400, ConstantTexts.UpdateInputFail);
        }

        return updatedInput;
    }

    public async Task<Input?> GetInputAsync(int id)
    {
        if (id <= 0)
        {
            _logger.LogError("Input id must be greater than zero");
            _notificationContext.AddNotification(400, "Input id must be greater than zero");
            return null;
        }

        var retrievedInput = await _inputRepository.Find(id);
        if (retrievedInput is null)
            _logger.LogError("Input not found");

        return retrievedInput;
    }
}
