using ConversationInsights.API.Endpoints;
using ConversationInsights.API.Extensions;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCategoryEndpoints();

app.UseHttpsRedirection();

app.Run();
