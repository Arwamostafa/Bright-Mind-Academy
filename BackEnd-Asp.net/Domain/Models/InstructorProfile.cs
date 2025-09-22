using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class InstructorProfile
    {
        public string Image { get; set; }


        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; } = new HashSet<StudentClassSubject>();


    }
}
