namespace ConversationInsights.Domain.Entities
{
    public class CallCategory
    {
        public Guid CallId { get; set; }
        public Guid CategoryId { get; set; }
        public Call Call { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
