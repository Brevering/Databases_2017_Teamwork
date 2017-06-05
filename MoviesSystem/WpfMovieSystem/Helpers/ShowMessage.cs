using System.Windows;

namespace WpfMovieSystem.Helpers
{
    public class ShowMessage
    {
        public static void ShowError(string msg)
        {
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            Window Owner = App.Current.MainWindow;
            string title = "Error";
            if (MessageBox.Show(Owner, msg, title, buttons, icon) == MessageBoxResult.OK)
            {
            }
        }

        public static void ShowInfo(string msg)
        {
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            Window Owner = App.Current.MainWindow;
            string title = "Attention";
            if (MessageBox.Show(Owner, msg, title, buttons, icon) == MessageBoxResult.OK)
            {
            }
        }

        public static bool ShowYesNo(string msg, string title)
        {
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Information;
            Window Owner = App.Current.MainWindow;
            if (MessageBox.Show(Owner, msg, title, buttons, icon) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
