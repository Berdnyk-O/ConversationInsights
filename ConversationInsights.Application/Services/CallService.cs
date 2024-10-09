using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application.Services
{
    public class CallService
    {
        private const string AudioFolderPath = "../ConversationInsights.Application/Audios/";

        private readonly IConversationInsightsRepository _repository;
        private readonly AudioLoader _audioLoader;
        private readonly SpeechRecognizer _speechRecognizer;

        public CallService(IConversationInsightsRepository repository,
            AudioLoader audioLoader,
            SpeechRecognizer speechRecognizer)
        {
            _repository = repository;
            _audioLoader = audioLoader;
            _speechRecognizer = speechRecognizer;
        }

        public async Task RecognizeCall(string audioUrl)
        {
            var format = audioUrl.Split('.')[^1];
            var audioPath = await _audioLoader.Load(audioUrl, AudioFolderPath, format);
            Console.WriteLine(audioPath);

            if(format == "mp3")
            {
                var converter = new Mp3ToWaveConverter();
                audioPath = converter.Convert(audioPath);
            }

            var text = _speechRecognizer.Recognize(audioPath);
            Console.WriteLine(text);

            var analyzer = new ConversationInsightsAnalyzer();
            analyzer.Analyze(text);
        }
    }
}
