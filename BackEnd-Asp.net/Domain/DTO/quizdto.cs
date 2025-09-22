
namespace Domain.DTO
{
    public class quizdto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool AssignedBefore { get; set; }
        public int TotalMarks { get; set; }

        public int LessonId { get; set; }

       
        public List<questiondto>? Questions { get; set; } = new List<questiondto>();

    }
}
