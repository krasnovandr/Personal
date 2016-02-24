namespace MusicRecognition.Models
{
    public class RecognizeResult
    {
        public Status status { get; set; }
        public int service_type { get; set; }
        public Metadata metadata { get; set; }
        public int result_type { get; set; }
    }
}
