using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationInsights.Application.DTOs
{
    public class UpdateCategoryDTO
    {
        public string? Title { get; set; }
        public string[] Points { get; set; } = null!;
    }
}
