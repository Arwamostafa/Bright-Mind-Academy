using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class option
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual question? Question { get; set; }
    }
}
