namespace ConversationInsights.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string[] Points { get; set; } = null!;
        public List<Call> Calls { get; } = [];
        public List<CallCategory> CallCategories { get; } = [];
       
    }
}
