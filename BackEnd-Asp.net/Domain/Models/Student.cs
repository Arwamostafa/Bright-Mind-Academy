//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Models
//{
//    public class Student
//    {
//        [Key]
//        public int StudentID { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public string Address { get; set; }
//        public string Phone { get; set; }
//        public string Gender { get; set; }
//        public int Age { get; set; }
//        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
//        public ICollection<SubjectStudent>?  subjectStudents { get; set; }= new HashSet<SubjectStudent>();
//    }
//}
