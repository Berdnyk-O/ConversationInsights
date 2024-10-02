using ConversationInsights.Application.DTOs;
using ConversationInsights.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConversationInsights.API.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/category", async (CategoryService categoryService) =>
            {
                var categories =  await categoryService.GetAllCategoriesAsync();
                
                return Results.Ok(categories);
            });

            app.MapPost("/category", async ([FromBody] NewCategoryDTO categoryDTO,
                CategoryService categoryService) =>
            {
                await categoryService.AddCategory(categoryDTO);
                
                return Results.Created();
            });

            app.MapPut("/category/{categoryId}", async ([FromRoute] Guid categoryId,
                [FromBody] NewCategoryDTO categoryDTO,
                CategoryService categoryService) =>
            {
                await categoryService.UpdateCategoryAsync(categoryId, categoryDTO);
                return Results.Ok();
            });

            app.MapDelete("/category/{categoryId}", async ([FromRoute] Guid categoryId,
                CategoryService categoryService) =>
            {
                await categoryService.DeleteCategoryAsync(categoryId);
                return Results.Ok();
            });
        }
    }
}
