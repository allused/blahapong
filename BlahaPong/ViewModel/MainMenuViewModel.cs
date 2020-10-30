using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace BlahaPong.ViewModel
{
    class MainMenuViewModel
    {

        public void StartGame(Window menu, bool isOnePlayerMode)
        {

            MainWindow win = new MainWindow(isOnePlayerMode);
            menu.Close();
            win.Show();
        }
    }
}
