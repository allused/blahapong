using BlahaPong.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace BlahaPong.View
{
    public partial class MainMenu : Window
    {
        private MainMenuViewModel _mainMenuViewModel = new MainMenuViewModel();
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OnePlayer(object sender, RoutedEventArgs e)
        {
            _mainMenuViewModel.StartGame(this, true);

        }
        
        private void TwoPlayer(object sender, RoutedEventArgs e)
        {
            _mainMenuViewModel.StartGame(this, false);
        }
        
        private void Credits(object sender, RoutedEventArgs e)
        {
            CreditWindow creditWindow = new CreditWindow();
            this.Close();
            creditWindow.Show();
            
        }
        
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}