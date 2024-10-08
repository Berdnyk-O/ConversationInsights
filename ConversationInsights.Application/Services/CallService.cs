using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application.Services
{
    public class CallService
    {
        private const string AudioFolderPath = "../ConversationInsights.Application/Audios/";
        private readonly IConversationInsightsRepository _repository;
        
        public CallService(IConversationInsightsRepository repository)
        {
            _repository = repository;
        }

        public async Task RecognizeCall(string audioUrl)
        {
            AudioLoader loader = new();

            var format = audioUrl.Split('.')[^1];
            var audioPath = await loader.Load(audioUrl, AudioFolderPath, format);
            Console.WriteLine(audioPath);

            if(format == "mp3")
            {
                audioPath = Mp3ToWaveConverter.Convert(audioPath);
            }

            var speechRecognizer = new SpeechRecognizer();
            var text = speechRecognizer.Recognize(audioPath);
            Console.WriteLine(text);
        }
    }
}
