namespace YouTubeDisco.Model.SearchEngine
{
    public class SearchResult
    {
        public SearchResult(string title, string description, string url, string thumbnailUri)
        {
            Title = title;
            Description = description;
            Url = url;
            ThumbnailUri = thumbnailUri;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ThumbnailUri { get; set; }
    }
}