using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        protected IActionResult TreatResponse(object? responseObject)
        {
            if (responseObject is null)
                return NotFound();

            return Ok(responseObject);
        }
    }
}
