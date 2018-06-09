using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.SearchEngine.YouTube;

namespace YouTubeDisco.ViewModels
{
    class SearchResultsVm : ViewModel
    {
        private readonly ISearchEngine _searchEngine = new YouTubeSearchEngine();

        public ObservableCollection<SearchResult> CreateNewCollection(string queryText)
        {
            return new SearchResultCollection(_searchEngine, queryText);
        }
    }

    class SearchResultCollection : ObservableCollection<SearchResult>, ISupportIncrementalLoading
    {
        private readonly string _query;
        private readonly ISearchEngine _searchEngine;
        private string _nextPageToken;
        private bool _hasMoreItems = true;
        public bool HasMoreItems => _hasMoreItems;

        public SearchResultCollection(ISearchEngine searchEngine, string query)
        {
            _searchEngine = searchEngine;
            _query = query;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher coreDispatcher = Window.Current.Dispatcher;

            return Task.Run(async () =>
            {
                SearchResultPage searchResultPage;

                searchResultPage = await _searchEngine.Search(_query, _nextPageToken);

                _nextPageToken = searchResultPage.NextPageToken;
                _hasMoreItems = _nextPageToken != null;

                coreDispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () => { searchResultPage.Results.ForEach(Add); });

                return new LoadMoreItemsResult {Count = (uint) (searchResultPage.Results.Count)};
            }).AsAsyncOperation();
        }
    }
}