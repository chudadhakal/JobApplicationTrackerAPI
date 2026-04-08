namespace JobApplicationTracker.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
