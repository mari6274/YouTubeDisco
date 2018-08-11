using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    public class YouTubeApi
    {
        private const string ApiKey = "AIzaSyAqsB7nHTG7IzX0DtVZYOFLbvYa8TJFGn4";
        private const string ApiAddress = "https://www.googleapis.com/youtube/v3/";
        private const string Resource = "search";

        private readonly HttpClient _httpClient;

        public YouTubeApi(HttpClient httpClient) => _httpClient = httpClient;

        internal async Task<SearchListResponse> List(string query, string pageToken)
        {
            var requestUri = ApiAddress + Resource + "?part=snippet&type=video&maxResults=5&key=" + ApiKey + "&q=" + query;
            if (pageToken!= null)
            {
                requestUri += "&pageToken=" + pageToken;
            }
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SearchListResponse>(content);
        }
    }
}