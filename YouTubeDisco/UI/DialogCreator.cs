using System;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace YouTubeDisco.UI
{
    public class DialogCreator
    {
        private readonly ResourceLoader _dialogResourceLoader = ResourceLoader.GetForCurrentView("Dialogs");

        public IAsyncOperation<ContentDialogResult> ShowMessageDialog(string contentTextKey)
        {
            var messageDialog = new ContentDialog()
            {
                Content = _dialogResourceLoader.GetString(contentTextKey),
                CloseButtonText = _dialogResourceLoader.GetString("Close")
            };
            return messageDialog.ShowAsync();
        }

        public IAsyncOperation<ContentDialogResult> ShowMessageDialog(string titleKey, string contentTextKey)
        {
            var messageDialog = new ContentDialog()
            {
                Title = _dialogResourceLoader.GetString(titleKey),
                Content = _dialogResourceLoader.GetString(contentTextKey),
                CloseButtonText = _dialogResourceLoader.GetString("Close")
            };
            return messageDialog.ShowAsync();
        }

        public IAsyncOperation<ContentDialogResult> ShowUnhandledExceptionMessage(string contentTextKey)
        {
            var messageDialog = new ContentDialog()
            {
                Title = _dialogResourceLoader.GetString("UnhandledExceptionTitle"),
                Content = _dialogResourceLoader.GetString("UnhandledExceptionContent"),
                CloseButtonText = _dialogResourceLoader.GetString("Close"),
                CloseButtonCommand = new ExitCommand()
            };
            return messageDialog.ShowAsync();
        }

        private class ExitCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                CoreApplication.Exit();
            }
        }
    }
}
