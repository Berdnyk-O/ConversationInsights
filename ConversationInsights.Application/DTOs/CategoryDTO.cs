using ConversationInsights.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationInsights.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string[] Points { get; set; } = null!;

        public CategoryDTO(Guid id,
            string title,
            string[] points)
        {
            Id = id; 
            Title = title;
            Points = points;
        }
    }
}
