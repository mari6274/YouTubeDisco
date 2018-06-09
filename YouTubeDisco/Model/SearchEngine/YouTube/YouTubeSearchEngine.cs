using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    internal class YouTubeSearchEngine : ISearchEngine
    {
        private readonly YouTubeApi api = new YouTubeApi();

        public async Task<SearchResultPage> Search(string query, string pageToken)
        {
            var searchListResponse = await api.List(query, pageToken);
            List<SearchResult> searchResults = searchListResponse.Items.Select(resource =>
                    new SearchResult(
                        resource.Snippet.Title,
                        resource.Snippet.Description,
                        "https://www.youtube.com/watch?v=" + resource.Id.VideoId))
                .ToList();
            return new SearchResultPage(searchResults, 
                searchListResponse.NextPageToken,
                searchListResponse.PrevPageToken);
        }
    }
}