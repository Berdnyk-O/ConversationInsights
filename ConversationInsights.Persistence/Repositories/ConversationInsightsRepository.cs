using ConversationInsights.Domain.Entities;
using ConversationInsights.Domain.Interfaces;
using ConversationInsights.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ConversationInsights.Persistence.Repositories
{
    public class ConversationInsightsRepository : IConversationInsightsRepository
    {
        private readonly ConversationInsightsDbContext _dbContext;

        public ConversationInsightsRepository(ConversationInsightsDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddCallAsync(Call call)
        {
            call.Id = Guid.NewGuid();
            await _dbContext.Calls.AddAsync(call);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync(); ;
        }

        public async Task DeleteCallById(Guid callId)
        {
            var existingCall = await  GetCallByIdAsync(callId);
            if(existingCall != null)
            {
                _dbContext.Calls.Remove(existingCall);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCategotyById(Guid categoryId)
        {
            var existingCategoty = await GetCategoryByIdAsync(categoryId);
            if (existingCategoty != null)
            {
                _dbContext.Categories.Remove(existingCategoty);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Call>> GetAllCallsAsync()
        {
            return await _dbContext.Calls.ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Call?> GetCallByIdAsync(Guid callId)
        {
            return await _dbContext.Calls.FirstOrDefaultAsync(c => c.Id == callId);
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task UpdateCallAsync(Call call)
        {
            var existingCall = await GetCallByIdAsync(call.Id);
            if(existingCall != null)
            {
                existingCall.Name = call.Name;
                existingCall.Location = call.Location;
                existingCall.EmotionalTone = call.EmotionalTone;
                existingCall.Text = call.Text;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategoty = await GetCategoryByIdAsync(category.Id);
            if (existingCategoty != null)
            {
                existingCategoty.Title = category.Title;
                existingCategoty.Points = category.Points;
            }

            await _dbContext.SaveChangesAsync();
        }

        Task<Category?> IConversationInsightsRepository.GetCallByIdAsync(Guid callId)
        {
            throw new NotImplementedException();
        }
    }
}
