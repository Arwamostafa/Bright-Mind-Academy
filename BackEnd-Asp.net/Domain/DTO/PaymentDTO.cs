using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }

        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }

        public int StudentID { get; set; }
        public string? StudentName { get; set; }

        public int? InstructorId { get; set; }

        public string?   InstructorName { get; set; }

        public decimal? Amount { get; set; }
        public bool? IsPaid { get; set; }
        public string? TransactionId { get; set; }
        public string? PaymentDate { get; set; }
    }
}
