using ConversationInsights.API.Endpoints;
using ConversationInsights.API.Extensions;
using ConversationInsights.Application;
using ConversationInsights.Application.Services;
using ConversationInsights.Domain.Interfaces;
using ConversationInsights.Persistence.Database;
using ConversationInsights.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConversationInsightsDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHostedService<MigrationHostedService>();

builder.Services.AddScoped<IConversationInsightsRepository, ConversationInsightsRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CallService>();
builder.Services.AddScoped<AudioLoader>();
builder.Services.AddScoped<SpeechRecognizer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCategoryEndpoints();
app.MapCallEndpoints();
app.UseHttpsRedirection();

app.Run();
