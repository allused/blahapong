using BlahaPong.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace BlahaPong.ViewModel
{
    class PauseWindowViewModel
    {
        public Window _pauseWindow;
        public static PauseWindowViewModel _pauseWindowViewModel = null;

        public static PauseWindowViewModel GetPauseWindowViewModel()
        {
           if (_pauseWindowViewModel != null)
            {
                return _pauseWindowViewModel;
            }
            return _pauseWindowViewModel = new PauseWindowViewModel();
        }

        private Window GetPauseWindow()
        {
            _pauseWindow = new PauseWindow();
            return _pauseWindow;
        }

        public void KeyPressedEvent()
        {
            Console.WriteLine(_pauseWindow);
            _pauseWindow.Close();
        }

        public void ShowPauseWindow()
        {
            GetPauseWindow().Show();
            _pauseWindow.Activate();
        }
    }
}
