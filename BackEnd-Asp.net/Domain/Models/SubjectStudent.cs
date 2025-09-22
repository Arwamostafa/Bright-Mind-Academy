using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SubjectStudent
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int StudentId { get; set; }
        public StudentProfile Student { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.Now;
    }
}
