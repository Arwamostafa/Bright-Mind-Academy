using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentClassSubject
    {
        [ForeignKey(nameof(Instructor))]
        public int InstructorID { get; set; }
        public virtual InstructorProfile Instructor { get; set; }

        [ForeignKey(nameof(Class))]
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }

        [ForeignKey(nameof(Track))]
        public int TrackID { get; set; }
        public virtual Track Track { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }


    }
}
