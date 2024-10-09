using ConversationInsights.Domain.Enums;
using System.Text.RegularExpressions;
using VaderSharp2;

namespace ConversationInsights.Application
{
    public class ConversationInsightsAnalyzer
    {
        private const string LocationFilePath = "../ConversationInsights.Application/Locations/countries.csv";
        public ConversationInsightsAnalyzer()
        {
                
        }

        public void Analyze(string text)
        {
            Console.WriteLine(text);
            Console.WriteLine(DefineEmotionalTone(text));
            DefineLocation(text);
        }

        private EmotionalTone DefineEmotionalTone(string text)
        {
            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();

            var results = analyzer.PolarityScores(text);

            double positiveScore = results.Positive;
            double negativeScore = results.Negative;
            double neutralScore = results.Neutral;
            double compoundScore = results.Compound;

            if (positiveScore > negativeScore && positiveScore > neutralScore)
            {
                return EmotionalTone.Positive;
            }
            else if (negativeScore > positiveScore && negativeScore > neutralScore)
            {
                return EmotionalTone.Negative;
            }
            else 
            {
                return EmotionalTone.Neutral;
            }
        }
        private string? DefineLocation(string text)
        {
            HashSet<string> countriesSet = new HashSet<string>();

            foreach (string line in File.ReadLines(LocationFilePath))
            {
                countriesSet.Add(line.Trim());
            }

            string pattern = string.Join("|", countriesSet.Select(Regex.Escape));

            Match match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                Console.WriteLine($"Знайдена країна: {match.Value}");
                
                return match.Value;
            }

            Console.WriteLine("Країну не знайдено у тексті.");
            
            return null;
        }
        private string? DefineName(string text) {  return null; }
        private string[] DefineCategories(string text) { return null; }
    }
}
