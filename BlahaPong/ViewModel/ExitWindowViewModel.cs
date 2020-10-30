using BlahaPong.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BlahaPong.ViewModel
{
    public class ExitWindowViewModel
    {
        private Window _exitWindow;
        private DispatcherTimer timer;
        private MainWindow _mainWindow;
        public static ExitWindowViewModel _exitWindowViewModel = null;

        public ExitWindowViewModel(DispatcherTimer timer, MainWindow mainWindow)
        {
            this.timer = timer;
            _mainWindow = mainWindow;
        }

        public static ExitWindowViewModel GetExitWindowViewModel(DispatcherTimer timer, MainWindow mainWindow)
        {
            if (_exitWindowViewModel == null)
            {
                return _exitWindowViewModel = new ExitWindowViewModel(timer, mainWindow);
            }
            return _exitWindowViewModel;
        }

        public void ContinueButtonClick()
        {
            _exitWindow.Close();
            timer.IsEnabled = !timer.IsEnabled;
        }

        public void MainMenuButtonClick()
        {
         
            MainMenu mainMenu = new MainMenu();
            _exitWindow.Close();
            _mainWindow.Close();
            mainMenu.Show();
        }


        public void ShowExitWindow(DispatcherTimer timer)
        {
            this.timer = timer;
            timer.IsEnabled = !timer.IsEnabled;
            ExitWindow exitWindow = new ExitWindow(this);
            this._exitWindow = exitWindow;
            exitWindow.Show();
        }

    }
}
