using System;
using Windows.UI.Xaml;

namespace YouTubeDisco.Config
{
    public class Settings
    {
        private const string StorageFolderNameKey = "StorageFolderName";
        private const string RemoveVideosKey = "RemoveVideos";
        private const string DefaultStorageFolderName = "YouTubeDisco";
        private const string ThemeKey = "Theme";
        private const string YouTubeApiKeyKey = "YouTubeApiKey";

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
            get => (string)Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageFolderNameKey];
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
            get => (bool)Windows.Storage.ApplicationData.Current.RoamingSettings.Values[RemoveVideosKey];
            set => Windows.Storage.ApplicationData.Current.RoamingSettings.Values[RemoveVideosKey] = value;
        }

        public ElementTheme Theme
        {
            get => Enum.TryParse(
                Windows.Storage.ApplicationData.Current.LocalSettings.Values[ThemeKey] as string,
                out ElementTheme theme) ? theme : ElementTheme.Default;
            set
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values[ThemeKey] = value.ToString();
                ThemeChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler ThemeChanged;

        public string YouTubeApiKey
        {
            get => (string)Windows.Storage.ApplicationData.Current.RoamingSettings.Values[YouTubeApiKeyKey];
            set
            {
                if (value != string.Empty)
                {
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values[YouTubeApiKeyKey] = value;
                }
            }
        }
    }
}