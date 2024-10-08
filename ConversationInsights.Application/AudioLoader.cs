using System.Reflection.Metadata;

namespace ConversationInsights.Application
{
    public class AudioLoader
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

                Console.WriteLine($"Failed to download audio. Status code: {response.StatusCode}");
                    
                return string.Empty;
                
            }
        }
    }
}
