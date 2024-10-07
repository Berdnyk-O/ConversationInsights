using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationInsights.Application
{
    public static class Mp3ToWaveConverter
    {
        public static void Convert(string mp3FilePath, string wavFilePath)
        {
            using (var mp3Reader = new Mp3FileReader(mp3FilePath))
            {
                var waveFormat = new WaveFormat(16000, 1);
                using (var resampler = new MediaFoundationResampler(mp3Reader, waveFormat))
                {
                    WaveFileWriter.CreateWaveFile(wavFilePath, resampler);
                }
            }
        }
    }
}
