using ConversationInsights.Application.DTOs;
using ConversationInsights.Domain.Entities;
using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application.Services
{
    public class CategoryService
    {
        private readonly IConversationInsightsRepository _repository;

        public CategoryService(IConversationInsightsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories =  await _repository.GetAllCategoriesAsync();
            
            var categoryDTOs = new List<CategoryDTO>(categories.Count);

            for (int i = 0; i < categories.Count; i++) {
                categoryDTOs.Add(
                    new CategoryDTO(categories[i].Id,
                        categories[i].Title,
                        categories[i].Points));
            }

            return categoryDTOs;
        }

        public async Task AddCategory(AddCategoryDTO categoryDTO)
        {
            var category = new Category(Guid.NewGuid(), categoryDTO.Title, categoryDTO.Points);
            await _repository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO categoryDTO)
        {
            var category = await _repository.GetCategoryByIdAsync(categoryId);
            if(category==null)
            {
                throw new NullReferenceException("The entity with the specified id does not exist");
            }

            if(categoryDTO.Title != null)
            {
                category.Title = categoryDTO.Title;
            }
            category.Points = categoryDTO.Points;
            await _repository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            await _repository.DeleteCategotyByIdAsync(categoryId);
        }
    }
}
