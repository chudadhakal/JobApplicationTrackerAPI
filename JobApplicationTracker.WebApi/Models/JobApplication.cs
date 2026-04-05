namespace JobApplicationTracker.WebApi.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public required DateTime ApplicationDate { get; set; }
            public string Notes { get; set; }
    }
}