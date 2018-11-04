using System.Collections.ObjectModel;

namespace YouTubeDisco.ViewModels
{
    public class TasksVm
    {
        public ObservableCollection<DownloadTask> Tasks { get; } = new ObservableCollection<DownloadTask>();
    }
}