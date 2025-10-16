//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Models
//{
//    public class Payment
//    {
//        [Key]
//        public int PaymentID { get; set; }

//        [Required]
//        public string PaymentIntentId { get; set; }

//        [Required]
//        public string ClientSecret { get; set; }
//        public decimal TotalPayMent { get; set; }

//        [ForeignKey("Subject")]
//        public int SubjectID { get; set; }

//        public Subject Subject { get; set; }

//        [ForeignKey("Student")]
//        public int StudentID { get; set; }           
//        public StudentProfile Student { get; set; }
        
//        public bool? IsSuccessful { get; set; }

//    }
//}
