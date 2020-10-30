using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace BlahaPong.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreditWindow : Window
    {
        public CreditWindow()
        {
            InitializeComponent();
            MediaPlayer mp = new MediaPlayer();
            mp.Open((new Uri(Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location)?
                .Replace(@"bin\Debug\netcoreapp3.1", @"Resources\creditsmusic.mp3") ?? throw new InvalidOperationException())));
            mp.Play();
        }
    }
}
