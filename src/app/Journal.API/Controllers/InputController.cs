using Journal.Domain;
using Journal.Service;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InputController : ControllerBase
    {
        private readonly IInputService _inputService;
        private readonly ILogger<InputController> _logger;

        public InputController(IInputService inputService, ILogger<InputController> logger)
        {
            _inputService = inputService;
            _logger = logger;
        }

        [HttpGet(Name = "GetInputs")]
        public async Task<IEnumerable<Input>> GetInputsAsync()
        {
            return await _inputService.GetAllInputs();
        }
    }
}
