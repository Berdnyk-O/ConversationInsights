using ConversationInsights.Domain.Enums;

namespace ConversationInsights.Domain.Entities
{
    public class Call
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Location { get; set; } = null!;
        public EmotionalTone EmotionalTone { get; set; }
        public string Text { get; set; } = null!;
        public List<Category> Categories { get; set; } = [];
        public List<CallCategory> CallCategories { get; } = [];
    }
}
