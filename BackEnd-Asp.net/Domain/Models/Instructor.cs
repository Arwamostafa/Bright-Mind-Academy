//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Models
//{
//    public class Instructor
//    {
//        [Key]
//        public int InstructorID { get; set; }
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public string  Address { get; set; }
//        public string Phone { get; set; }
//        public string Gender { get; set; }
//        public int Age { get; set; }
//        public string ImageURL { get; set; }

//        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
//        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; } = new HashSet<StudentClassSubject>();

//    }
//}
