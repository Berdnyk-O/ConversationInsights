using ConversationInsights.Domain.Entities;

namespace ConversationInsights.Domain.Interfaces
{
    public interface IConversationInsightsAnalyzer
    {
        void PopulateCallDetails(string text, Call call, List<Category> categories);

    }
}
