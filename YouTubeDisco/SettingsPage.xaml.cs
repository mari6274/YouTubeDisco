namespace YouTubeDisco
{
    public partial class SettingsPage : BasePage
    {
        public override void PostInjectionInitialize()
        {
            base.PostInjectionInitialize();
            DataContext = Settings;
        }

        public SettingsPage()
        {
            this.InitializeComponent();
        }
    }
}