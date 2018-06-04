using System.Collections.Generic;

namespace YouTubeDisco.Model.SearchEngine
{
    public interface ISearchEngine
    {
        List<SearchResult> Search(string query);
    }
}