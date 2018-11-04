using System.Collections.ObjectModel;
using YouTubeDisco.ViewModels;

namespace YouTubeDisco
{
    public sealed partial class TasksPage : BasePage
    {
        private TasksVm _tasksVm;
        public TasksVm TasksVm
        {
            set => _tasksVm = value;
        }

        public ObservableCollection<DownloadTask> Tasks
        {
            get => _tasksVm.Tasks;
        }

        public TasksPage()
        {
            InitializeComponent();
        }
    }
}
