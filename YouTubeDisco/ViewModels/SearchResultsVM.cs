using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.SearchEngine.YouTube;

namespace YouTubeDisco.ViewModels
{
    class SearchResultsVM : ViewModel
    {
        private readonly ISearchEngine _searchEngine = new YouTubeSearchEngine();

        private readonly ObservableCollection<SearchResult> _searchResults = new ObservableCollection<SearchResult>();

        public ObservableCollection<SearchResult> SearchResults => _searchResults;

        public void Query(string queryText)
        {
            _searchResults.Clear();
            _searchEngine.Search(queryText).ForEach(_searchResults.Add);
            OnPropertyChanged();
        }
    }
}