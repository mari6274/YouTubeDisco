using System.ComponentModel;
using System.Runtime.CompilerServices;
using YouTubeDisco.Model.SearchEngine;

namespace YouTubeDisco.ViewModels
{
    public class DownloadTask : INotifyPropertyChanged
    {
        private State _state = State.CREATED;

        public DownloadTask(SearchResult searchResult) => SearchResult = searchResult;

        public State State {
            get => _state;
            private set {
                _state = value;
                OnPropertyChanged();
            }
        }

        public SearchResult SearchResult { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Start() => State = State.STARTED;

        public void Finish()
        {
            if (State != State.FAILED)
            {
                State = State.FINISHED;
            }
        }

        public void Fail() => State = State.FAILED;
    }
}