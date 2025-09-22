using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CreatedSubjectDTO
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public decimal Price { get; set; }
        public string? PaymentIntenId { get; set; }
        public string? ClientSecret { get; set; }
        public int InstructorID { get; set; }
        public int ClassID { get; set; }
        public int TrackID { get; set; }

    }
}
