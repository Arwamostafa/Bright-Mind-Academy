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
        public virtual Subject Subject { get; set; }

        public int StudentId { get; set; }
        public virtual StudentProfile Student { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? PaymentDate { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.Now;
    }
}
