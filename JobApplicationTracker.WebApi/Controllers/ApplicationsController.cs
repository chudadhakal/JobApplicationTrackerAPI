using JobApplicationTracker.WebApi.Data;
using JobApplicationTracker.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Inserting data into the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieving all job applications data from database 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetApplications")]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await _context.JobApplications.OrderByDescending(a => a.ApplicationDate).ToListAsync();
          
            return Ok(applications);
        }

        /// <summary>
        /// Retrieving job application by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetApplicationById")]
        public async Task<IActionResult> GetApplicationById([FromQuery] int id)
        {
            var applications = await _context.JobApplications.FindAsync(id);

            return Ok(applications);
        }

        /// <summary>
        /// Retrieves job applications filtered by status, ordered by application date in descending order.
        /// </summary>
        /// <param name="status">The status value to filter job applications.</param>
        /// <returns>An IActionResult containing the filtered list of job applications.</returns>
        [HttpGet]
        [Route("GetApplicationsByStatus")]
        public async Task<IActionResult> GetApplicationsByStatus([FromQuery] string status)
        {
            var applications = await _context.JobApplications.Where(a=>a.Status == status).OrderByDescending(a => a.ApplicationDate).ToListAsync();
            return Ok(applications);
        }

        /// <summary>
        /// Delete application by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteApplication")]
        public async Task<IActionResult> DeleteApplication([FromQuery] int id)
        {
            var applications = await _context.JobApplications.Where(a=>a.Id == id).ExecuteDeleteAsync();
            // return success msg

            if (applications == 0)
            {
                return NotFound($"Application with ID(id) not found");
            }

            var ReturnMessge = "Success";
            return Ok(ReturnMessge);
        }
    }
}
