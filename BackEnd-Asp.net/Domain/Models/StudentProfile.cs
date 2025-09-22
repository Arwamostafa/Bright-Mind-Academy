using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class StudentProfile
    {
        public int Age { get; set; }

        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public ICollection<SubjectStudent>? subjectStudents { get; set; } = new HashSet<SubjectStudent>();
    }
}
