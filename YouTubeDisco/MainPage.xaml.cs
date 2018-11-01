﻿using System;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YouTubeDisco.Model.AudioExtractor;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.VideoDownloader;
using YouTubeDisco.ViewModels;

namespace YouTubeDisco
{
    public sealed partial class MainPage : BasePage
    {
        private SearchResultsVm _searchResultsVm;
        private IVideoDownloader _videoDownloader;
        private IAudioExtractor _audioExtractor;
        private readonly ResourceLoader _resourceLoader;

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
            InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView("MainPage");
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
                .CreateFolderAsync(Settings.StorageFolderName, CreationCollisionOption.OpenIfExists);
            var videoFile = await _videoDownloader.DownloadVideo(searchResult, youTubeDiscoVideoFolder);

            var youTubeDiscoMusicFolder = await KnownFolders.MusicLibrary
                .CreateFolderAsync(Settings.StorageFolderName, CreationCollisionOption.OpenIfExists);
            var extractionResult = await _audioExtractor.ExtractAudio(videoFile, youTubeDiscoMusicFolder);

            if (extractionResult == ExtractionResult.CodecNotFound)
            {
                ShowMessageDialog("MissingMpeg3Encoder");
            }

            if (extractionResult == ExtractionResult.Failed)
            {
                ShowMessageDialog("UnexpectedError");
            }

            if (Settings.RemoveVideos)
            {
                await videoFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }

            searchResult.DownloadProgressIsActive = false;
        }

        private void ShowMessageDialog(string contentTextKey)
        {
            var messageDialog = new MessageDialog(_resourceLoader.GetString(contentTextKey));
            messageDialog.Commands.Add(new UICommand(_resourceLoader.GetString("Close")));
            messageDialog.ShowAsync();
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private async void OpenStorageLocation_OnClick(object sender, RoutedEventArgs e)
        {
            var storageFolder = await KnownFolders.MusicLibrary
                .CreateFolderAsync(Settings.StorageFolderName, CreationCollisionOption.OpenIfExists);
            Launcher.LaunchFolderAsync(storageFolder);
        }
    }
}