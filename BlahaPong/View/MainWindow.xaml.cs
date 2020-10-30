using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlahaPong.Model;
using BlahaPong.ViewModel;
using Path = System.IO.Path;


namespace BlahaPong
{
    /// <summMainWindow_OnKeyUpaction logic for MainWindow.xaml>
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public MainWindow(bool isOnePlayerMode)
        {
            InitializeComponent();
            vm = new MainWindowViewModel(canv, isOnePlayerMode, ScoreSeparator, this);
            vm.StartGameLoop();
            //rect.PreviewMouseLeftButtonDown += (sender, args) MessageBox.Show("Yo mamma");
           
            vm.SetWindowHeightAndWidth(canv.Height - 10, canv.Width - 10);
           

        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            vm.KeydownEvent(e,0);
        }
        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            vm.KeyUpEvent(e);
        }

    }
}