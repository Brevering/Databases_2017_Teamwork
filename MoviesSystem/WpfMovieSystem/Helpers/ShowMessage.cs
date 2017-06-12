using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfMovieSystem.Helpers
{
    public class ShowMessage
    {
        private static MessageBoxButton okBtn = MessageBoxButton.OK;
        private static MessageBoxImage errIcon = MessageBoxImage.Error;
        private static MessageBoxButton yesnoBtn = MessageBoxButton.YesNo;
        private static MessageBoxImage infoIcon = MessageBoxImage.Information;

        public static void ShowError(string msg)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                Window Owner = App.Current.MainWindow;
                string title = "Error";
                if (MessageBox.Show(Owner, msg, title, okBtn, errIcon) == MessageBoxResult.OK)
                {
                }
            }));
        }

        public static void ShowInfo(string msg)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                Window Owner = App.Current.MainWindow;
                string title = "Attention";
                if (MessageBox.Show(Owner, msg, title, okBtn, infoIcon) == MessageBoxResult.OK)
                {
                }
            }));            
        }

        public static bool ShowYesNo(string msg, string title)
        {
            bool result = false;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                Window Owner = App.Current.MainWindow;
                if (MessageBox.Show(Owner, msg, title, yesnoBtn, infoIcon) == MessageBoxResult.Yes)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }));

            return result;       
        }
    }
}
