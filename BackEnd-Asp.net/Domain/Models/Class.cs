using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 50")]
        public string ClassName { get; set; }

        //[JsonIgnore]
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; } = new HashSet<StudentClassSubject>();
    }
}
