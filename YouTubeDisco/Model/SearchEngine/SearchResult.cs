using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YouTubeDisco.Model.SearchEngine
{
    public class SearchResult : INotifyPropertyChanged
    {
        private bool _downloadProgressIsActive = false;

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

        public bool DownloadProgressIsActive
        {
            get { return _downloadProgressIsActive; }
            set
            {
                _downloadProgressIsActive = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}