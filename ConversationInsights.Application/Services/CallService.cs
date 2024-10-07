using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application.Services
{
    public class CallService
    {
        private const string AudiosPath = "../ConversationInsights.Application/Audios/{0}";
        private readonly IConversationInsightsRepository _repository;
        
        public CallService(IConversationInsightsRepository repository)
        {
            _repository = repository;
        }

        public void RecognizeCall(string audioUrl)
        {
            var speechRecognizer = new SpeechRecognizer();
            var text = speechRecognizer.Recognize(string.Format(AudiosPath, "Audio_zone_Beating_stress.wav"));
            Console.WriteLine(text);
        }
    }
}
