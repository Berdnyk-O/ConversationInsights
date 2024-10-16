﻿using ConversationInsights.Application.DTOs;
using ConversationInsights.Domain.Entities;
using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application.Services
{
    public class CallService
    {
        private const string AudioFolderPath = "../ConversationInsights.Application/Audios/";

        private readonly IConversationInsightsRepository _repository;
        private readonly IAudioLoader _audioLoader;
        private readonly ISpeechRecognizer _speechRecognizer;
        private readonly IConversationInsightsAnalyzer _conversationInsightsAnalyzer;

        public CallService(IConversationInsightsRepository repository,
            IAudioLoader audioLoader,
            ISpeechRecognizer speechRecognizer,
            IConversationInsightsAnalyzer conversationInsightsAnalyzer)
        {
            _repository = repository;
            _audioLoader = audioLoader;
            _speechRecognizer = speechRecognizer;
            _conversationInsightsAnalyzer = conversationInsightsAnalyzer;
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

            var categories = await _repository.GetAllCategoriesAsync();

            _conversationInsightsAnalyzer.PopulateCallDetails(text, call, categories);

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
