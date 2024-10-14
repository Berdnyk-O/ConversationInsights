namespace ConversationInsights.Domain.Interfaces
{
    public interface ISpeechRecognizer
    {
        string Recognize(string audioPath);
    }
}
