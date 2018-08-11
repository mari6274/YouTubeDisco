using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.SearchEngine.YouTube;

namespace YouTubeDisco.ViewModels
{
    public class SearchResultsVm
    {
        private readonly ISearchEngine _searchEngine;

        public SearchResultsVm(ISearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
        }

        public ObservableCollection<SearchResult> CreateNewCollection(string queryText, ProgressBar progressBar)
        {
            return new SearchResultCollection(_searchEngine, queryText, progressBar);
        }
    }

    internal class SearchResultCollection : ObservableCollection<SearchResult>, ISupportIncrementalLoading
    {
        private readonly string _query;
        private readonly ProgressBar _progressBar;
        private readonly ISearchEngine _searchEngine;
        private string _nextPageToken;
        public bool HasMoreItems { get; private set; } = true;

        public SearchResultCollection(ISearchEngine searchEngine, string query, ProgressBar progressBar)
        {
            _searchEngine = searchEngine;
            _query = query;
            _progressBar = progressBar;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher coreDispatcher = Window.Current.Dispatcher;

            _progressBar.Visibility = Visibility.Visible;
            return Task.Run(async () =>
            {
                SearchResultPage searchResultPage;

                searchResultPage = await _searchEngine.Search(_query, _nextPageToken);

                _nextPageToken = searchResultPage.NextPageToken;
                HasMoreItems = _nextPageToken != null;

                coreDispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        searchResultPage.Results.ForEach(Add);
                        _progressBar.Visibility = Visibility.Collapsed;
                    });

                return new LoadMoreItemsResult {Count = (uint) (searchResultPage.Results.Count)};
            }).AsAsyncOperation();
        }
    }
}