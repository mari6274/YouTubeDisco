using System.Collections.Generic;
using System.Threading.Tasks;

namespace YouTubeDisco.Model.SearchEngine
{
    public interface ISearchEngine
    {
        Task<SearchResultPage> Search(string query, string pageToken);
    }
}