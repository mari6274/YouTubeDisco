using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Popups;

namespace YouTubeDisco.UI
{
    public class DialogCreator
    {
        private readonly ResourceLoader _dialogResourceLoader = ResourceLoader.GetForCurrentView("Dialogs");

        public IAsyncOperation<IUICommand> ShowMessageDialog(string contentTextKey)
        {
            var messageDialog = new MessageDialog(_dialogResourceLoader.GetString(contentTextKey));
            messageDialog.Commands.Add(new UICommand(_dialogResourceLoader.GetString("Close")));
            return messageDialog.ShowAsync();
        }

        public IAsyncOperation<IUICommand> ShowUnhandledExceptionMessage(string contentTextKey)
        {
            var messageDialog = new MessageDialog(
                _dialogResourceLoader.GetString("UnhandledExceptionContent"),
                _dialogResourceLoader.GetString("UnhandledExceptionTitle"));
            messageDialog.Commands.Add(new UICommand(_dialogResourceLoader.GetString("Close"), 
                command => CoreApplication.Exit()));
            return messageDialog.ShowAsync();
        }
    }
}
