namespace ConversationInsights.API.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/category", async () =>
            {
                return Results.NotFound();
            });

            app.MapPost("/category", async () =>
            {
                return Results.NotFound();
            });

            app.MapPut("/category/{category_id}", async (string category_id) =>
            {
                return Results.NotFound();
            });

            app.MapDelete("/category/{category_id}", async (string category_id) =>
            {
                return Results.NotFound();
            });
        }
    }
}
