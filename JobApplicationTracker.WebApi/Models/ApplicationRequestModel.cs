using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.WebApi.Models
{
    public class ApplicationRequestModel
    {
        
        public required string CompanyName { get; set; }
        public required string JobTitle { get; set; }
        public required DateTime ApplicationDate { get; set; }
        public required string Status { get; set; }
        public required string Notes { get; set; }
        public int UserId { get; set; }

    }
}
