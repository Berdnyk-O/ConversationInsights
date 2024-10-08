using NAudio.Wave;

namespace ConversationInsights.Application
{
    public static class Mp3ToWaveConverter
    {
        public static string Convert(string mp3FilePath)
        {
            string waveFilePath = Path.ChangeExtension(mp3FilePath, ".wav");

            using (var mp3Reader = new Mp3FileReader(mp3FilePath))
            {
                var waveFormat = new WaveFormat(16000, 1);
                using (var resamper = new MediaFoundationResampler(mp3Reader, waveFormat))
                {
                    WaveFileWriter.CreateWaveFile(waveFilePath, resamper);
                }
            }
            
            File.Delete(mp3FilePath);

            return waveFilePath;
        }
    }
}
