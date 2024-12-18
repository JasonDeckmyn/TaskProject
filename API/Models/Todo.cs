namespace aspnet.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UrgencyLevel Urgency { get; set; }
        public bool IsComplete { get; set; } = false;
    }

    public enum UrgencyLevel
    {
        Low,
        Medium,
        High
    }
}