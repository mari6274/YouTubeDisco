using System.ComponentModel;
using System.Runtime.CompilerServices;
using YouTubeDisco.Annotations;

namespace YouTubeDisco.Config
{
    public class Settings : INotifyPropertyChanged
    {
        private const string StorageFolderNameKey = "StorageFolderName";
        private const string RemoveVideosKey = "RemoveVideos";
        private const string DefaultStorageFolderName = "YouTubeDisco";

        public Settings()
        {
            InitDefaultsIfNeeded();
        }

        private static void InitDefaultsIfNeeded()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if ((roamingSettings.Values[StorageFolderNameKey] as string ?? string.Empty) == string.Empty)
            {
                roamingSettings.Values[StorageFolderNameKey] = DefaultStorageFolderName;
            }

            if (roamingSettings.Values[RemoveVideosKey] == null)
            {
                roamingSettings.Values[RemoveVideosKey] = false;
            }
        }

        public string StorageFolderName
        {
            get => (string) Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageFolderNameKey];
            set
            {
                if (value != string.Empty)
                {
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageFolderNameKey] = value;
                }
            }
        }

        public bool RemoveVideos
        {
            get => (bool) Windows.Storage.ApplicationData.Current.RoamingSettings.Values[RemoveVideosKey];
            set => Windows.Storage.ApplicationData.Current.RoamingSettings.Values[RemoveVideosKey] = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}