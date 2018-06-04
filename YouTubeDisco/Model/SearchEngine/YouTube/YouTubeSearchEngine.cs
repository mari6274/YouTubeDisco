using System.Collections.Generic;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    internal class YouTubeSearchEngine : ISearchEngine
    {
        public List<SearchResult> Search(string query)
        {
            return new List<SearchResult>(new[]
            {
                new SearchResult("x", "x", "x"),
                new SearchResult("y", "y", "y"),
                new SearchResult("z", "z", "z")
            });
        }
    }
}