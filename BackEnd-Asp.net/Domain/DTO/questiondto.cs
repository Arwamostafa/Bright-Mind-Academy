

namespace Domain.DTO
{
    public class questiondto
    {
        public int id { get; set; }
        public string Content { get; set; }
        public int mark { get; set; }
        public int QuizId { get; set; }
        public virtual List<optiondto> Options { get; set; } = new List<optiondto>();



    }
}
