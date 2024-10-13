namespace ConversationInsights.Application.DTOs
{
    public class UpdateCategoryDTO
    {
        public string? Title { get; set; }
        public string[] Points { get; set; } = null!;
    }
}
