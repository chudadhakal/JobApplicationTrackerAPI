using JobApplicationTracker.WebApi.Data;
using JobApplicationTracker.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
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
        //API for proifle page, return user info and job applications
        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> Profile([FromQuery] int userId)
        {
            var userWithApplications = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (userWithApplications == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new
            {
                id = userWithApplications.Id,
                firstName = userWithApplications.FirstName,
                lastName = userWithApplications.LastName,
                email = userWithApplications.Email,
                jobApplications = userWithApplications.JobApplications

            });

        } //APi for update user info
        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromQuery] int userId, [FromBody] UserRequestModel model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            // Update user properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;
            // Save changes to the database
            await _context.SaveChangesAsync();
            return Ok(new { message = "Profile updated successfully.", user });
        }
    }
}
