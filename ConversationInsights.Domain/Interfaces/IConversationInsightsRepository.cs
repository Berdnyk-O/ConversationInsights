using ConversationInsights.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationInsights.Domain.Interfaces
{
    public interface IConversationInsightsRepository
    {
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid categoryId);
        Task DeleteCategotyByIdAsync(Guid categoryId);
        Task AddCallAsync(Call call);
        Task UpdateCallAsync(Call call);
        Task<List<Call>> GetAllCallsAsync();
        Task<Call?> GetCallByIdAsync(Guid callId);
        Task DeleteCallByIdAsync(Guid callId);

    }
}
