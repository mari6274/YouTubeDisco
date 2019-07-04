using System;

namespace YouTubeDisco.Model.VideoDownloader
{
    [Serializable]
    public class VideoDownloadException : Exception
    {
        public VideoDownloadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}