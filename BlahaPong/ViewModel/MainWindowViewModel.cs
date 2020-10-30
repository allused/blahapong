using System;
using System.Collections.Generic;
using System.Reflection;
using BlahaPong.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using BlahaPong.Model;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace BlahaPong.ViewModel
{
    public class MainWindowViewModel
    {
        private Canvas canv;
        private int tickCounter = 0;
        private bool isOnePlayerMode;
        private List<Gift> gifts = new List<Gift>();
        private Random random = new Random();

        private MainWindow _mainWindow;
        public MainWindowViewModel(Canvas canv, bool isOnePlayerMode,  TextBox ScoreSeparator, MainWindow mainWindow)
        {
            this.isOnePlayerMode = isOnePlayerMode;
            _mainWindow = mainWindow;

            _ball = new Ball(380, 197, 10, 20, 20, isOnePlayerMode);
            

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace(@"bin\Debug\netcoreapp3.1", @"Resources\images\tablebackground.png") ?? throw new InvalidOperationException()));
            canv.Background = ib;
            
            if (!isOnePlayerMode)
            {
                ScoreSeparator.Visibility = Visibility.Visible;
                canv.Children.Add(playerTwo.Rectangle);
                canv.Children.Add(PlayerTwoScore);
            }
            
            canv.Children.Add(playerOne.Rectangle);
            
            canv.Children.Add(_ball.BallItem);
            canv.Children.Add(PauseImage);
            canv.Children.Add(PlayerOneScore);
            this.canv = canv;
            balls.Add(_ball);
        }


        ExitWindowViewModel _exitWindowViwModel;


        private readonly static int SCORE_BOX_HEIGHT = 45;

        private readonly static int SCORE_BOX_WIDTH = 46;

        private readonly static int SCORE_BOX_FONT_SIZE = 36;

        private readonly static string SCORE_BOX_BASICS_TEXT = "0";
        
        private readonly static FontFamily SCORE_BOX_FONT_FAMILY = new FontFamily("Bahnschrift SemiBold");

        private double WindowHeight;

        private double WindowWidth;
        public Paddle playerOne { get; } = new Paddle(20, 115, 10, 100, 10, true);
        public Paddle playerTwo { get; } = new Paddle(770, 115, 10, 100, 10, false);

        private List<Ball> balls = new List<Ball>();
        public Ball _ball { get; } 

        public void SetWindowHeightAndWidth(double height, double width)
        {
            this.WindowHeight = height;
            this.WindowWidth = width;
        }


        private TextBox PlayerOneScore { get; set; } = new TextBox() {
            Height = SCORE_BOX_HEIGHT,
            Width = SCORE_BOX_WIDTH,
            Text= SCORE_BOX_BASICS_TEXT,
            TextWrapping = TextWrapping.Wrap,
            FontSize = SCORE_BOX_FONT_SIZE,
            FontFamily = SCORE_BOX_FONT_FAMILY,
            IsReadOnly = true,
            BorderThickness = new Thickness(0),
            TextAlignment = TextAlignment.Center,
            Background = Brushes.Transparent
        };
        private TextBox PlayerTwoScore { get; set; } = new TextBox() {
            Height = SCORE_BOX_HEIGHT,
            Width = SCORE_BOX_WIDTH,
            Text = SCORE_BOX_BASICS_TEXT,
            TextWrapping = TextWrapping.Wrap,
            FontSize = SCORE_BOX_FONT_SIZE,
            FontFamily = SCORE_BOX_FONT_FAMILY,
            IsReadOnly = true,
            BorderThickness = new Thickness(0),
            TextAlignment = TextAlignment.Center,
            Background = Brushes.Transparent
        };

        private Image PauseImage { get; } = new Image()
        {
            Height = 206,
            Width = 708,
            Visibility = Visibility.Hidden, 
            Source = new BitmapImage(new Uri(Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location)?
                .Replace(@"bin\Debug\netcoreapp3.1", @"Resources\pauseTwo.png") ?? throw new InvalidOperationException()))
        };
   
        
        private DispatcherTimer timer;
        public void KeydownEvent(KeyEventArgs e, double botBorder){
            switch (e.Key)
            {
                case Key.W:
                    playerOne.Direction = -1;
                    playerOne.PaddleMove = true;
                    break;
                case Key.S:
                    playerOne.Direction = 1;
                    playerOne.PaddleMove = true;
                    break;
                case Key.Down:
                    playerTwo.Direction = 1;
                    playerTwo.PaddleMove = true;
                    break;
                case Key.Up:
                    playerTwo.Direction = -1;
                    playerTwo.PaddleMove = true;
                    break;
                case Key.Space:
                    if (PauseImage.Visibility == Visibility.Visible)
                    {
                        PauseImage.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        PauseImage.Visibility = Visibility.Visible;
                    }
                    timer.IsEnabled = !timer.IsEnabled;
                    //PauseWindowViewModel.GetPauseWindowViewModel().ShowPauseWindow();
                    break;
                case Key.Escape:
                    //ShowExitMessageBox();
                    _exitWindowViwModel = ExitWindowViewModel.GetExitWindowViewModel(timer, _mainWindow);
                    _exitWindowViwModel.ShowExitWindow(timer);
                    break;    
            }
        }
         

        

        //PauseWindowViewModel _pauseWindowViwModel = new PauseWindowViewModel();

        public void ShowExitMessageBox()
        {
            var result = MessageBox.Show("Do you want to quit ?", "Goodbye?", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        
        public void KeyUpEvent(KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S)
            {
                playerOne.PaddleMove = false;
                playerOne.Direction = 0;
            }
            else if (e.Key == Key.Down || e.Key == Key.Up)
            {
                playerTwo.PaddleMove = false;
                playerTwo.Direction = 0;
            }
        }

        public void StartGameLoop()
        {
            if (!isOnePlayerMode)
            {
                _ball.SetPlayerTextBox(PlayerOneScore, PlayerTwoScore);
                _ball.SetPlayers(playerOne, playerTwo);
                Canvas.SetLeft(PlayerTwoScore, 412);
                Canvas.SetTop(PlayerTwoScore, 10);
            }

            _ball.SetPlayers(playerOne);
            _ball.SetPlayerTextBox(PlayerOneScore);

            Canvas.SetLeft(PauseImage, 46);
            Canvas.SetTop(PauseImage, 104);
            
            Canvas.SetLeft(PlayerOneScore, 338);
            Canvas.SetTop(PlayerOneScore, 10);
            
            
            
            timer = new DispatcherTimer();
            timer.Tick += UpdateGame;
            timer.Interval = new TimeSpan(0, 0, 0,0,25);
            timer.Start();
        }

        private void UpdateGame(object sender, EventArgs e)
        {
            ++tickCounter;
            if (tickCounter >= 40 * 10 || balls.Count == 0)
            {
                if (random.Next(100) <= 80 && balls.Count != 0)
                {
                    AddGift();
                }
                AddBall();
            }

            foreach (var gift in gifts)
            {
                if (!gift.Move(WindowHeight, WindowWidth))
                {
                    canv.Children.Remove(gift.Rectangle);
                    gifts.Remove(gift);
                    break;
                }
            }
            
            foreach (var ball in balls)
            {
                if (!ball.Move(WindowHeight, WindowWidth))
                {
                    NextRound();
                    if (isOnePlayerMode)
                    {
                        playerOne.Score = 0;
                        PlayerOneScore.Text = "0";
                    }

                    break;
                }
            }

            foreach (var gift in gifts)
            {
                if (gift.IsActive)
                {
                    canv.Children.Remove(gift.Rectangle);
                    if (gift.ActiveTime >= 40 * 7)
                    {
                        gift.GiftDeactivation();
                        gifts.Remove(gift);
                        break;
                    }
                    if (!(gift.ActiveTime >= 40 * 7))
                    {
                        gift.ActiveTime++;
                    }
                }
            }

            playerOne.Move(WindowHeight, WindowWidth);
            if (!isOnePlayerMode) playerTwo.Move(WindowHeight, WindowWidth);
           
        }

        private void AddGift()
        {
            var gift = new DoubleSizeGift(1, 30, 30, random.Next(50,750), 0, ref balls);
            gifts.Add(gift);
            canv.Children.Add(gift.Rectangle);
        }

        private void AddBall()
        {
            Ball newBall = new Ball(380, 197, 10, 20, 20, isOnePlayerMode);
            if (this.isOnePlayerMode)
            {
                newBall.SetPlayers(playerOne);
                newBall.SetPlayerTextBox(PlayerOneScore);
            }
            else
            {
                newBall.SetPlayers(playerOne, playerTwo);
                newBall.SetPlayerTextBox(PlayerOneScore, PlayerTwoScore);
            }

            balls.Add(newBall);
            canv.Children.Add(newBall.BallItem);
            tickCounter = 0;
        }
        public void NextRound()
        {
            foreach (var ball in balls)
            {
                canv.Children.Remove(ball.BallItem);
            }

            foreach (var gift in gifts)
            {
                canv.Children.Remove(gift.Rectangle);
            }

            playerOne.Rectangle.Height = 100;
            playerTwo.Rectangle.Height = 100;
            gifts.Clear();
            balls.Clear();
            Thread.Sleep(2000);
            tickCounter = 0;
        }

    }
}