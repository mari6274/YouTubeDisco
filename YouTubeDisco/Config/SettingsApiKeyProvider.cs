using YouTubeDisco.Model.SearchEngine.YouTube;

namespace YouTubeDisco.Config
{
    public class SettingsApiKeyProvider : IApiKeyProvider
    {
        private readonly Settings _settings;

        public SettingsApiKeyProvider(Settings settings) => _settings = settings;

        public string getKey() => _settings.YouTubeApiKey;
    }
}