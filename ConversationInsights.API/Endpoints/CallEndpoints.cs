﻿using ConversationInsights.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConversationInsights.API.Endpoints
{
    public static class CallEndpoints
    {
        public static void MapCallEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/call/{callId}", async (Guid callId,
                 CallService callService) =>
            {
                var call = await callService.GetCallById(callId);
                
                return call;
            });

            app.MapPost("/call", async ([FromBody] string audioUrl,
                CallService callService) =>
            {
                try
                {
                    var callId = await callService.RecognizeCall(audioUrl);
                    
                    return Results.Created($"/call/{callId}", callId);
                }
                catch (Exception ex) 
                {
                    return Results.UnprocessableEntity(ex.Message);
                }
            });
        }
    }
}
