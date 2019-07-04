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

        public async Task<StorageFile> DownloadVideo(SearchResult searchResult, StorageFolder outputFolder)
        {
            return await Task.Run(async () =>
            {
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(searchResult.Url);
                var file = await outputFolder.CreateFileAsync(video.FullName, CreationCollisionOption.GenerateUniqueName);

                try
                {
                    await FileIO.WriteBufferAsync(file, video.GetBytes().AsBuffer());
                }
                catch (Exception e)
                {
                    file.DeleteAsync();
                    throw new VideoDownloadException("Video could not be downloaded.", e);
                }
                return file;
            });
        }
    }
}