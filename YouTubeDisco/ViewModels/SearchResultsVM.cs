using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.UI;

namespace YouTubeDisco.ViewModels
{
    public class SearchResultsVm
    {
        private readonly ISearchEngine _searchEngine;
        private readonly DialogCreator _dialogCreator;

        public SearchResultsVm(ISearchEngine searchEngine, DialogCreator dialogCreator)
        {
            _searchEngine = searchEngine;
            _dialogCreator = dialogCreator;
        }

        public ObservableCollection<SearchResult> CreateNewCollection(string queryText, ProgressBar progressBar)
        {
            return new SearchResultCollection(_searchEngine, queryText, progressBar, _dialogCreator);
        }
    }

    internal class SearchResultCollection : ObservableCollection<SearchResult>, ISupportIncrementalLoading
    {
        private readonly string _query;
        private readonly ProgressBar _progressBar;
        private readonly DialogCreator _dialogCreator;
        private readonly ISearchEngine _searchEngine;
        private string _nextPageToken;
        public bool HasMoreItems { get; private set; } = true;

        public SearchResultCollection(ISearchEngine searchEngine, string query, ProgressBar progressBar, DialogCreator dialogCreator)
        {
            _searchEngine = searchEngine;
            _query = query;
            _progressBar = progressBar;
            _dialogCreator = dialogCreator;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher coreDispatcher = Window.Current.Dispatcher;

            _progressBar.Visibility = Visibility.Visible;
            return Task.Run(async () =>
            {
                SearchResultPage searchResultPage;

                searchResultPage = await _searchEngine.Search(_query, _nextPageToken, () =>
                {
                    coreDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            _dialogCreator.ShowMessageDialog("SearchEngineError");
                        });
                });

                _nextPageToken = searchResultPage.NextPageToken;
                HasMoreItems = _nextPageToken != null;

                coreDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        searchResultPage.Results.ForEach(Add);
                        _progressBar.Visibility = Visibility.Collapsed;
                    });

                return new LoadMoreItemsResult { Count = (uint)(searchResultPage.Results.Count) };
            }).AsAsyncOperation();
        }
    }
}