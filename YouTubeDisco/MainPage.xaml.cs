using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using VideoLibrary;
using YouTubeDisco.Model.AudioExtractor;
using YouTubeDisco.Model.AudioExtractor.MediaTranscoder;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.VideoDownloader;
using YouTubeDisco.Model.VideoDownloader.VideoLibrary;
using YouTubeDisco.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace YouTubeDisco
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SearchResultsVm _searchResultsVm;
        private IVideoDownloader _videoDownloader;
        private IAudioExtractor _audioExtractor;

        public IAudioExtractor AudioExtractor
        {
            set => _audioExtractor = value;
        }

        public IVideoDownloader VideoDownloader
        {
            set => _videoDownloader = value;
        }

        public SearchResultsVm SearchResultsVm
        {
            set
            {
                _searchResultsVm = value;
                DataContext = value;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SearchBox_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SearchResultsListView.ItemsSource =
                _searchResultsVm.CreateNewCollection(args.QueryText, SearchResultsProgressBar);
        }

        private async void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var searchResult = (SearchResult) button.DataContext;
            searchResult.DownloadProgressIsActive = true;

            var youTubeDiscoVideoFolder = await KnownFolders.VideosLibrary
                .CreateFolderAsync("YouTubeDisco", CreationCollisionOption.OpenIfExists);
            var videoFile = await _videoDownloader.DownloadVideo(searchResult, youTubeDiscoVideoFolder);

            var youTubeDiscoMusicFolder = await KnownFolders.MusicLibrary
                .CreateFolderAsync("YouTubeDisco", CreationCollisionOption.OpenIfExists);
            await _audioExtractor.ExtractAudio(videoFile, youTubeDiscoMusicFolder);

            searchResult.DownloadProgressIsActive = false;
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private async void OpenStorageLocation_OnClick(object sender, RoutedEventArgs e)
        {
            var storageFolder = await KnownFolders.MusicLibrary
                .CreateFolderAsync("YouTubeDisco", CreationCollisionOption.OpenIfExists);
            Launcher.LaunchFolderAsync(storageFolder);
        }
    }
}