using System;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YouTubeDisco.Model.AudioExtractor;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.VideoDownloader;
using YouTubeDisco.UI;
using YouTubeDisco.ViewModels;

namespace YouTubeDisco
{
    public sealed partial class MainPage : BasePage
    {
        private SearchResultsVm _searchResultsVm;
        private TasksVm _tasksVm;
        private IVideoDownloader _videoDownloader;
        private IAudioExtractor _audioExtractor;
        private DialogCreator _dialogCreator;

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

        public TasksVm TasksVm
        {
            set => _tasksVm = value;
        }
        public DialogCreator DialogCreator
        {
            set => _dialogCreator = value;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchBox_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SearchResultsListView.ItemsSource = 
                _searchResultsVm.CreateNewCollection(args.QueryText, SearchResultsProgressBar);
        }

        private async void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var searchResult = (SearchResult)button.DataContext;

            button.Visibility = Visibility.Collapsed;

            var downloadTask = new DownloadTask(searchResult);
            _tasksVm.Tasks.Add(downloadTask);

            downloadTask.Start();

            var youTubeDiscoVideoFolder = await KnownFolders.VideosLibrary
                .CreateFolderAsync(Settings.StorageFolderName, CreationCollisionOption.OpenIfExists);
            try
            {
                var videoFile = await _videoDownloader.DownloadVideo(searchResult, youTubeDiscoVideoFolder);

                var youTubeDiscoMusicFolder = await KnownFolders.MusicLibrary
               .CreateFolderAsync(Settings.StorageFolderName, CreationCollisionOption.OpenIfExists);
                var extractionResult = await _audioExtractor.ExtractAudio(videoFile, youTubeDiscoMusicFolder);

                if (extractionResult == ExtractionResult.CodecNotFound)
                {
                    _dialogCreator.ShowMessageDialog("MissingMpeg3Encoder");
                    downloadTask.Fail();
                }

                if (extractionResult == ExtractionResult.Failed)
                {
                    _dialogCreator.ShowMessageDialog("UnexpectedError");
                    downloadTask.Fail();
                }

                if (Settings.RemoveVideos)
                {
                    await videoFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }

                downloadTask.Finish();
            }
            catch (VideoDownloadException)
            {
                downloadTask.Fail();
            }
           
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private async void OpenStorageLocationButton_OnClick(object sender, RoutedEventArgs e)
        {
            var storageFolder = await KnownFolders.MusicLibrary
                .CreateFolderAsync(Settings.StorageFolderName, CreationCollisionOption.OpenIfExists);
            Launcher.LaunchFolderAsync(storageFolder);
        }

        private void TasksListButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TasksPage));
        }
    }
}