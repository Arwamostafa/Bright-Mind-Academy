using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Title { get; set; }
        [MaxLength(700)]
        public  string? Description { get; set; }

        public int UnitId { get; set; }

        public virtual Unit? Unit { get; set; }
        [MaxLength(700)]
        public string? VideoUrl { get; set; }
        [MaxLength(700)]
        public string? PdfUrl { get; set; }
        [MaxLength(700)]
        public string? AssigmentUrl { get; set; }

        public DateTime? AssigmentDeadLine { get; set; }

        public virtual quiz? Quiz { get; set; }


    }
}
 