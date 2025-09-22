using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Track
    {
        [Key]
        public int TrackID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 50")]
        public string TrackName { get; set; }

        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; } = new HashSet<StudentClassSubject>();

    }
}
