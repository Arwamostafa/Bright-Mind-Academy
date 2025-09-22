namespace Domain.Models
{
    public class quiz
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool AssignedBefore { get; set; }
        public int TotalMarks { get; set; }
        public virtual ICollection<question> Questions { get; set; } = new HashSet<question>();
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

    }
}
