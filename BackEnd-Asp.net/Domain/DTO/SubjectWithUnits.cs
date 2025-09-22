using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class SubjectWithUnits
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public decimal Price { get; set; }

        public int InstructorID { get; set; }
        public int ClassID { get; set; }
        public int TrackID { get; set; }

        public string? ClassName { get; set; }
        public string? TrackName { get; set; }

        public string? InstructorName { get; set; }

        public List<UnitDto>? Units { get; set; } = new List<UnitDto>();


    }
}