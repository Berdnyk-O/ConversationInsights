namespace ConversationInsights.Domain.Interfaces
{
    public interface IAudioLoader
    {
        Task<string> Load(string url, string audioFolderPath, string format);

    }
}
