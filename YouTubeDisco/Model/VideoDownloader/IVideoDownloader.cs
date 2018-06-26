using System.Threading.Tasks;
using Windows.Storage;
using YouTubeDisco.Model.SearchEngine;

namespace YouTubeDisco.Model.VideoDownloader
{
    public interface IVideoDownloader
    {
        Task DownloadVideo(SearchResult searchResult, StorageFolder outputFile);
    }
}