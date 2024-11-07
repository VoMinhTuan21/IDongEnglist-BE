namespace IDonEnglist.Application.Utils
{
    public static class AudioFileValidator
    {
        public static bool IsAudioFile(string fileName)
        {
            string[] audioExtensions = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".wma", ".m4a" };
            string extension = Path.GetExtension(fileName).ToLower();

            foreach (var audioExtension in audioExtensions)
            {
                if (extension == audioExtension)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
