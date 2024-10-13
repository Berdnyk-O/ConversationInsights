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

        public async Task<Guid> AddCategory(AddCategoryDTO categoryDTO)
        {
            var category = new Category(Guid.NewGuid(), categoryDTO.Title.Trim(), categoryDTO.Points);
            
            var categories = await _repository.GetAllCategoriesAsync();
            bool categoryExists = categories.Any(x => x.Title.Equals(category.Title, StringComparison.OrdinalIgnoreCase));
            
            if(categoryExists)
            {
                throw new InvalidOperationException("A category with this title already exists.");
            }
            if (category.Points.Length<1)
            {
                throw new InvalidOperationException("The points array cannot be empty.");
            }

            await _repository.AddCategoryAsync(category);

            return category.Id;
        }

        public async Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO categoryDTO)
        {
            var category = await _repository.GetCategoryByIdAsync(categoryId);
            if(category==null)
            {
                throw new NullReferenceException("The entity with the specified id does not exist.");
            }

            if(!string.IsNullOrEmpty(categoryDTO.Title))
            {
                var categories = await _repository.GetAllCategoriesAsync();
                bool categoryExists = categories.Any(x => x.Title.Equals(categoryDTO.Title, StringComparison.OrdinalIgnoreCase));

                if (categoryExists)
                {
                    throw new InvalidOperationException("A category with this title already exists.");
                }
                if (category.Points.Length < 1)
                {
                    throw new InvalidOperationException("The points array cannot be empty.");
                }

                category.Title = categoryDTO.Title.Trim();
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
