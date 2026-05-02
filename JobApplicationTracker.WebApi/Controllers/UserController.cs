using JobApplicationTracker.WebApi.Data;
using JobApplicationTracker.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // constructor 
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserRequestModel model)
        {
            //validate parameter
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            // convert model to entity
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                CreatedAt = DateTime.Now,
                IsVerified = false //always set default to false when creating user
                //JobApplications = model.JobApplications
            };

            // add to database
            _context.Add(user);
            // save changes
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            return Ok(new
            {
                message = "Login successful.",
                user = new
                {
                    id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email
                }
            });



        }
    }
}