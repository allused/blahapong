using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace BlahaPong.Model
{
    public class Paddle : Sprite
    {
        public Rectangle Rectangle {get;} = new Rectangle();
        public int Direction {get; set;}
        public bool PaddleMove {get; set;}

        public int Score { get; set;}

        ImageBrush ib = new ImageBrush();



        public Paddle(int xPosition, int yPosition, int speed, int height, int width, bool isPlayerOne) : base(speed)
        {
            if (isPlayerOne)
            {
                ib.ImageSource = new BitmapImage(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace(@"bin\Debug\netcoreapp3.1", @"Resources\images\atesz.png") ?? throw new InvalidOperationException()));
            }
            else
            {
                ib.ImageSource = new BitmapImage(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace(@"bin\Debug\netcoreapp3.1", @"Resources\images\lacitwo.png") ?? throw new InvalidOperationException()));
            }

            Rectangle.Fill = Brushes.Black;
            Rectangle.Width = width;
            Rectangle.Height = height;
            Rectangle.Fill = ib;
            Canvas.SetLeft(Rectangle, xPosition);
            Canvas.SetTop(Rectangle, yPosition);
            Score = 0;
        }

        public override bool Move(double windowHeight, double windowWidth)
        {
            if (PaddleMove && CanMove(windowHeight))
            {
                Canvas.SetTop(Rectangle, Canvas.GetTop(Rectangle) + Direction * speed);
            }
            return true;
        }
        
        private bool CanMove(double botBorder)
        {
            if (Canvas.GetTop(Rectangle) < 0 && Direction == -1)
            {
                return false;
            } else if (Canvas.GetTop(Rectangle) + Rectangle.Height > botBorder && Direction == 1)
            {
                return false;
            }
            return true;
        }
    }
}