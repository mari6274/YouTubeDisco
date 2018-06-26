using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using VideoLibrary;
using YouTubeDisco.Model.SearchEngine;

namespace YouTubeDisco.Model.VideoDownloader.VideoLibrary
{
    public class VideoLibraryVideoDownloader : IVideoDownloader
    {

        public async Task DownloadVideo(SearchResult searchResult, StorageFolder youTubeDiscoFolder)
        {
            await Task.Run(async () =>
            {
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(searchResult.Url);
                var file = await youTubeDiscoFolder.CreateFileAsync(video.FullName, CreationCollisionOption.GenerateUniqueName);
                await FileIO.WriteBufferAsync(file, video.GetBytes().AsBuffer());
            });
        }
    }
}