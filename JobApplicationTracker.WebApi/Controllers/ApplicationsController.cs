using JobApplicationTracker.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        [HttpPost]
        [Route("CreateApplication")]
        public IActionResult CreateApplication([FromQuery] ApplicationRequestModel model)
        {
            //validate parameter
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(model);
        }
    }
}
