using JobApplicationTracker.WebApi.Data;
using JobApplicationTracker.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobApplicationTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // constructor 
        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateApplication")]
        public async Task<IActionResult> CreateApplication(ApplicationRequestModel model)
        {
            //validate parameter
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            // convert model to entity
            var application = new JobApplication
            {
                CompanyName = model.CompanyName,
                Position = model.JobTitle,
                ApplicationDate = model.ApplicationDate,
                Status = model.Status,
                Notes = model.Notes
            };

            // add to database
            _context.Add(application);
            // save changes
            await _context.SaveChangesAsync();

            return Ok(application);
        }
    }
}
