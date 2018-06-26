﻿using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using VideoLibrary;
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
        private readonly SearchResultsVm _searchResultsVm = new SearchResultsVm();
        private readonly IVideoDownloader _videoDownloader = new VideoLibraryVideoDownloader();

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = _searchResultsVm;
        }

        private void SearchBox_OnQuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            SearchResultsListView.ItemsSource =
                _searchResultsVm.CreateNewCollection(args.QueryText, SearchResultsProgressBar);
        }

        private async void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var searchResult = (SearchResult) button.DataContext;
            searchResult.DownloadProgressIsActive = true;

            var youTubeDiscoFolder = await KnownFolders.VideosLibrary
                .CreateFolderAsync("YouTubeDisco", CreationCollisionOption.OpenIfExists);

            await _videoDownloader.DownloadVideo(searchResult, youTubeDiscoFolder);
            searchResult.DownloadProgressIsActive = false;
        }
        
    }
}