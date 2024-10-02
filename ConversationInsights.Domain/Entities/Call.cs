using ConversationInsights.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationInsights.Domain.Entities
{
    public class Call
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public EmotionalTone EmotionalTone { get; set; }
        public string Text { get; set; } = null!;
        public List<Category> Categories { get; } = [];
        public List<CallCategory> CallCategories { get; } = [];
    }
}
