using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application
{
    public class AudioLoader : IAudioLoader
    {
        private readonly HttpClient _httpClient;
        
        public AudioLoader()
        {
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("Accept", "audio/mpeg, audio/wav, audio/x-wav, audio/x-pn-wav, audio/vnd.wav");
        }

        public async Task<string> Load(string url, string audioFolderPath, string format) 
        {
            var audioName = $"{Guid.NewGuid()}.{format}";
            var audioPath = Path.Combine(audioFolderPath, audioName);

            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        await File.WriteAllBytesAsync(audioPath, await response.Content.ReadAsByteArrayAsync());
                    }
            
                    return audioPath;
                }
                
                throw new ArgumentException("The provided URL is invalid. Cannot download file from URL");
            }
        }
    }
}
