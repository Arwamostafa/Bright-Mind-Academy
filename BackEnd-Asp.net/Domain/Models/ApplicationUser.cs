using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }

        public StudentProfile? StudentProfile { get; set; }
        public InstructorProfile? InstructorProfile { get; set; }
        public AdminProfile? AdminProfile { get; set; }



    }

    public class ApplicationRole : IdentityRole<int> { }
}
