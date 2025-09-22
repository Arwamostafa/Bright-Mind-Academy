using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class question
    {
        public int id { get; set; }
        public string Content { get; set; }
        public int mark { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public virtual quiz? Quiz { get; set; }
        public virtual ICollection<option>? Options { get; set; } = new HashSet<option>();

    }
}
