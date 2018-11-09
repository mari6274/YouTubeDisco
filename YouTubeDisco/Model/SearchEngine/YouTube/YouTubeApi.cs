using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    public class YouTubeApi
    {
        private const string ApiAddress = "https://www.googleapis.com/youtube/v3/";
        private const string Resource = "search";

        private readonly HttpClient _httpClient;
        private readonly IApiKeyProvider _apiKeyProvider;

        public YouTubeApi(HttpClient httpClient, IApiKeyProvider apiKeyProvider)
        {
            _httpClient = httpClient;
            _apiKeyProvider = apiKeyProvider;
        }

        internal async Task<SearchListResponse> List(string query, string pageToken, ErrorDelegate errorDelegate)
        {
            var requestUri = ApiAddress + Resource + "?part=snippet&type=video&maxResults=5&key=" + _apiKeyProvider.getKey() + "&q=" + query;
            if (pageToken != null)
            {
                requestUri += "&pageToken=" + pageToken;
            }
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<SearchListResponse>(content);
            }
            else
            {
                errorDelegate();
                return new SearchListResponse();
            }
        }
    }
}