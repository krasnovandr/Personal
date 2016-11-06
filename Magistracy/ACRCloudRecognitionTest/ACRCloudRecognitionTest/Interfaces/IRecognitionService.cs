using MusicRecognition.Models;

namespace MusicRecognition.Interfaces
{
    public interface IRecognitionService
    {
        RecognizeResult Recognise(string filePath, int startSecond, int audioLengthSeconds);
    }
}
