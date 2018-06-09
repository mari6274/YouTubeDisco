using System;
using System.Collections.Generic;
using Windows.Devices.Printers;

namespace YouTubeDisco.Model.SearchEngine
{
    public class SearchResultPage
    {
        public SearchResultPage(List<SearchResult> results, string nextPageToken, string prevPageToken)
        {
            Results = results;
            NextPageToken = nextPageToken;
            PrevPageToken = prevPageToken;
        }

        public List<SearchResult> Results { get; set; }
        public string NextPageToken { get; set; }
        public string PrevPageToken { get; set; }
    }
}