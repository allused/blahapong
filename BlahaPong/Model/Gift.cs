using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BlahaPong.Model
{
    public abstract class Gift : Sprite
    {
        public Rectangle Rectangle { get; } = new Rectangle();
        public List<Ball> Balls { get; }
        protected Paddle player;
        public bool IsActive { get; set;} = false;
        public int ActiveTime { get; set; }

        protected Gift(int speed, int height, int width, int xPosition, int yPosition, ref List<Ball> balls) : base(speed)
        {
            Rectangle.Fill = Brushes.Gold;
            Rectangle.Width = width;
            Rectangle.Height = height;
            Canvas.SetLeft(Rectangle, xPosition);
            Canvas.SetTop(Rectangle, yPosition);
            Balls = balls;
        }

        public override bool Move(double Height, double Width)
        {
            Canvas.SetTop(Rectangle, Canvas.GetTop(Rectangle) + speed);
            foreach (var ball in Balls)
            {
                if (Canvas.GetLeft(Rectangle) < Canvas.GetLeft(ball.BallItem) 
                    && Canvas.GetLeft(Rectangle) + Rectangle.Width > Canvas.GetLeft(ball.BallItem) 
                    && Canvas.GetTop(Rectangle) < Canvas.GetTop(ball.BallItem)
                    && Canvas.GetTop(Rectangle) + Rectangle.Height > Canvas.GetTop(ball.BallItem)
                    && player == null && ball.LastTouchedPlayer != null)
                {
                    player = ball.LastTouchedPlayer;
                    GiftActivation();
                    IsActive = true;
                }
            }
            if (Canvas.GetTop(Rectangle) >= Height)
            {
                return false;
            }
            return true;
        }

        public abstract void GiftDeactivation();

        public abstract void GiftActivation();
    }
}