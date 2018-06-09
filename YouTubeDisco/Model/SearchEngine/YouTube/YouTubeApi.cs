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
        private const string API_KEY = "AIzaSyAqsB7nHTG7IzX0DtVZYOFLbvYa8TJFGn4";
        private const string API_ADDRESS = "https://www.googleapis.com/youtube/v3/";
        private const string RESOURCE = "search";

        private readonly HttpClient _httpClient = new HttpClient();

        internal async Task<SearchListResponse> List(string query, string pageToken)
        {
            var requestUri = API_ADDRESS + RESOURCE + "?part=snippet&type=video&maxResults=5&key=" + API_KEY + "&q=" + query;
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