using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Unit 
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }
        [MaxLength(700)]
        public string? Description { get; set; }

        public int SubjectId { get; set; }

        public virtual Subject? Subject { get; set; }

        public List<Lesson> Lessons { get; set; }


    }
}
