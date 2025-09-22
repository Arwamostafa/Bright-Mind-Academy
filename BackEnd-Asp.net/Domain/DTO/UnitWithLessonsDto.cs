using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UnitWithLessonsDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }
        [MaxLength(700)]
        public string? Description { get; set; }
        public List<LessonDto>? Lessons { get; set; }
    }
}
