using System.Linq;
using System.Threading.Tasks;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    internal class YouTubeSearchEngine : ISearchEngine
    {
        private const string YoutubeWatchUrl = "https://www.youtube.com/watch?v=";
        private readonly YouTubeApi _api;

        public YouTubeSearchEngine(YouTubeApi api) => _api = api;

        public async Task<SearchResultPage> Search(string query, string pageToken, ErrorDelegate errorDelegate)
        {
            var searchListResponse = await _api.List(query, pageToken, errorDelegate);
            var searchResults = searchListResponse.Items.Select(resource =>
                    new SearchResult(
                        resource.Snippet.Title,
                        resource.Snippet.Description,
                        YoutubeWatchUrl + resource.Id.VideoId,
                        resource.Snippet.Thumbnails.Default.Url))
                .ToList();
            return new SearchResultPage(searchResults, 
                searchListResponse.NextPageToken,
                searchListResponse.PrevPageToken);
        }
    }
}