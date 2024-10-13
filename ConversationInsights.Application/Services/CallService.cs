using ConversationInsights.Application.DTOs;
using ConversationInsights.Domain.Entities;
using ConversationInsights.Domain.Interfaces;
using static System.Net.Mime.MediaTypeNames;

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


        public async Task<CallDTO?> GetCallById(Guid Id)
        {
            var call = await _repository.GetCallByIdAsync(Id);
            var callDTO = new CallDTO
            {
                Id = call.Id,
                Name = call.Name,
                Location = call.Location,
                EmotionalTone = call.EmotionalTone.ToString(),
                Text = call.Text,
                Categories = call.Categories.Select(x=>x.Title).ToArray()
            }; 

            return callDTO;
        }

        public async Task<Guid> RecognizeCall(string audioUrl)
        {
            var extension = Path.GetExtension(audioUrl);
            if (extension != ".mp3" && extension!=".wav")
            {
                throw new ArgumentException("The provided URL is invalid. URL must point to an mp3 or wav file.");    
            }

            var audioPath = await LoadAudio(audioUrl);

            var text = _speechRecognizer.Recognize(audioPath);

            Call call = new();
            call.Id = Guid.Parse(Path.GetFileNameWithoutExtension(audioPath));
            Console.WriteLine(call.Id);
            var categories = await _repository.GetAllCategoriesAsync();

            var analyzer = new ConversationInsightsAnalyzer();
            analyzer.PopulateCallDetails(text, call, categories);

            Console.WriteLine(call.Name);
            Console.WriteLine(call.Id);
            Console.WriteLine(call.Text);

            await _repository.AddCallAsync(call);

            return call.Id;
        }

        private async Task<string> LoadAudio(string audioUrl)
        {
            var format = audioUrl.Split('.')[^1];
            var audioPath = await _audioLoader.Load(audioUrl, AudioFolderPath, format);

            if (format == "mp3")
            {
                var converter = new Mp3ToWaveConverter();
                audioPath = converter.Convert(audioPath);
            }

            return audioPath;
        }
    }
}
