using System.ComponentModel;
using System.Runtime.CompilerServices;
using YouTubeDisco.Annotations;

namespace YouTubeDisco.Config
{
    public class Settings : INotifyPropertyChanged
    {
        private const string StorageFolderNameKey = "StorageFolderName";
        private const string DefaultStorageFolderName = "YouTubeDisco";

        public Settings()
        {
            InitDefaultsIfNeeded();
        }

        private static void InitDefaultsIfNeeded()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if ((string) (roamingSettings.Values[StorageFolderNameKey] ?? string.Empty) == string.Empty)
            {
                roamingSettings.Values[StorageFolderNameKey] = DefaultStorageFolderName;
            }
        }

        public string StorageFolderName
        {
            get => Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageFolderNameKey] as string;
            set
            {
                if (value != string.Empty)
                {
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageFolderNameKey] = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}