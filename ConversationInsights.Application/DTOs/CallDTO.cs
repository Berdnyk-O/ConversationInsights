using ConversationInsights.Domain.Entities;
using ConversationInsights.Domain.Enums;

namespace ConversationInsights.Application.DTOs
{
    public class CallDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Location { get; set; } = null!;
        public string EmotionalTone { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string[] Categories { get; set; } = [];

    }
}
