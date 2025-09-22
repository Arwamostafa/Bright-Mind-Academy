using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string InstructorName { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public string SubjectPrice { get; set; }
    }
}
