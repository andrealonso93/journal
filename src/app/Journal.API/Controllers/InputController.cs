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

        public InputController(IInputService inputService)
        {
            _inputService = inputService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInputsAsync()
        {
            var inputs = await _inputService.GetAllInputsAsync();
            return TreatResponse(inputs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInput([FromBody] string entryText)
        {
            var createdInput = await _inputService.CreateInputAsync(entryText);
            return TreatResponse(createdInput);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInput(int id, [FromBody] string newEntry)
        {
            var updatedInput = await _inputService.UpdateInputAsync(id, newEntry);
            return TreatResponse(updatedInput);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInput(int id)
        {
            var input = await _inputService.GetInputAsync(id);
            return TreatResponse(input);
        }

        private IActionResult TreatResponse(object? responseObject)
        {
            if (responseObject is null)
                return NotFound();

            return Ok(responseObject);
        }
    }
}
