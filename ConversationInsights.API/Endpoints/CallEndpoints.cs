using ConversationInsights.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConversationInsights.API.Endpoints
{
    public static class CallEndpoints
    {
        public static void MapCallEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/call/{callId}", async (Guid callId) =>
            {
                return Results.Ok();
            });

            app.MapPost("/call", async ([FromBody] string audioUrl,
                CallService callService) =>
            {
                callService.RecognizeCall(audioUrl);
                return Results.Ok();
            });
        }
    }
}
