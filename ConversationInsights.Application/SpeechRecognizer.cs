using Vosk;
using NAudio.Wave;
using System.Text;
using System.Text.Json;
using ConversationInsights.Domain.Interfaces;

namespace ConversationInsights.Application
{
    public class SpeechRecognizer : ISpeechRecognizer
    {
        private const string ModelPath = "../ConversationInsights.Application/VoskModel";

        public SpeechRecognizer()
        {
        }

        public string Recognize(string audioPath)
        {
            using (var model = new Model(ModelPath))
            {
                StringBuilder sb = new StringBuilder();
                using (var waveIn = new WaveFileReader(audioPath))
                {
                    string? text;
                    var rec = new VoskRecognizer(model, waveIn.WaveFormat.SampleRate);
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = waveIn.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (rec.AcceptWaveform(buffer, bytesRead))
                        {
                            text = ExtractTextFromJson(rec.Result());
                            sb.Append(text);
                        }
                    }

                    text = ExtractTextFromJson(rec.FinalResult());
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }

        private string? ExtractTextFromJson(string jsonResult)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonResult))
            {
                if (doc.RootElement.TryGetProperty("text", out JsonElement textElement))
                {
                    return textElement.GetString();
                }
            }

            return string.Empty; 
        }
    }
}
