using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 20")]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "more than max length that is allowed")]
        public string SubjectDescription { get; set; }
        [Required]
        [Range(0, 10000.000, ErrorMessage = "Price must be between 0 and 1000")]
        [DataType("decimal(1,10000)")]
        public decimal Price { get; set; }

      


        [ForeignKey(nameof(Instructor))]
        public int InstructorID { get; set; }
        public virtual InstructorProfile Instructor { get; set; }
        public List<Unit>? Units { get; set; } = new List<Unit>();

        public virtual ICollection<SubjectStudent> subjectStudents { get; set; }
        public virtual StudentClassSubject StudentClassSubject { get; set; } 

    }
}
