using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Autofac;
using YouTubeDisco.Model.AudioExtractor;
using YouTubeDisco.Model.AudioExtractor.MediaTranscoder;
using YouTubeDisco.Model.SearchEngine;
using YouTubeDisco.Model.SearchEngine.YouTube;
using YouTubeDisco.Model.VideoDownloader;
using YouTubeDisco.Model.VideoDownloader.VideoLibrary;
using YouTubeDisco.ViewModels;
using YouTubeDisco.UI;
using Windows.UI.Xaml;

namespace YouTubeDisco.Config
{
    public class AutofacFrame : Frame
    {
        private readonly IContainer _container;

        public AutofacFrame(Application app)
        {
            var containerBuilder = new ContainerBuilder();

            //Dependencies registration
            containerBuilder.RegisterType<VideoLibraryVideoDownloader>()
                .As<IVideoDownloader>()
                .SingleInstance();
            containerBuilder.RegisterType<MediaTranscoderAudioExtractor>()
                .As<IAudioExtractor>()
                .SingleInstance();
            containerBuilder.RegisterType<YouTubeSearchEngine>()
                .As<ISearchEngine>()
                .SingleInstance();
            containerBuilder.RegisterType<SearchResultsVm>();
            containerBuilder.RegisterType<YouTubeSearchEngine>()
                .As<ISearchEngine>()
                .SingleInstance();
            containerBuilder.RegisterType<SettingsApiKeyProvider>()
                .As<IApiKeyProvider>()
                .SingleInstance();
            containerBuilder.RegisterType<YouTubeApi>()
                .SingleInstance();
            containerBuilder.RegisterType<HttpClient>()
                .SingleInstance();
            containerBuilder.RegisterType<Settings>()
                .SingleInstance();
            containerBuilder.RegisterType<TasksVm>()
                .SingleInstance();
            containerBuilder.RegisterType<DialogCreator>()
                .SingleInstance();
            //Dependencies registration

            _container = containerBuilder.Build();

            _container.InjectUnsetProperties(app);

            InitializeFrame();
        }

        private void InitializeFrame()
        {
            var settings = _container.Resolve<Settings>();
            RequestedTheme = settings.Theme;
            settings.ThemeChanged += (sender, args) => { RequestedTheme = settings.Theme; };
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            _container.InjectUnsetProperties(newContent);
            (newContent as BasePage)?.PostInjectionInitialize();
        }
    }
}