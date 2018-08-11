using System.Linq;
using System.Threading.Tasks;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    internal class YouTubeSearchEngine : ISearchEngine
    {
        private const string YoutubeWatchUrl = "https://www.youtube.com/watch?v=";
        private readonly YouTubeApi _api = new YouTubeApi();

        public async Task<SearchResultPage> Search(string query, string pageToken)
        {
            var searchListResponse = await _api.List(query, pageToken);
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