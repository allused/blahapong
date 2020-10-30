using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;


namespace BlahaPong.Model
{
    public class Ball : Sprite
    {
        public Ellipse BallItem { get; }= new Ellipse();
        private int xDirection;
        private int yDirection;
        // player references for collision check and later for scores
        private Paddle playerOne;
        private TextBox playerOneTextBox;

        private Paddle playerTwo;
        private TextBox playerTwoTextBox;
        public Paddle LastTouchedPlayer { get; set; }
        private List<int> xCoords = new List<int>(){-1, 0, 1};
        private List<int> yCoords = new List<int>(){-1, 1};
        private Random rand = new Random();

        private bool isOnePlayerMode;

        ImageBrush ib = new ImageBrush();
        public Ball(int xPosition, int yPosition, int speed, int height, int width, bool isOnePlayerMode) : base(speed)
        {
            ib.ImageSource = new BitmapImage(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace(@"bin\Debug\netcoreapp3.1", @"Resources\ccedited.png") ?? throw new InvalidOperationException()));

            xDirection = -1;
            yDirection = 1;
            int RandXi = rand.Next(xCoords.Count);
            xDirection = xCoords[RandXi];
            int RandYi = rand.Next(yCoords.Count);
            yDirection = yCoords[RandYi];
            BallItem.Fill = Brushes.Red;
            BallItem.Stroke = Brushes.Black;
            BallItem.Width = width;
            BallItem.Height = height;
            BallItem.Fill = ib;
            Canvas.SetLeft(BallItem, xPosition);
            Canvas.SetTop(BallItem, yPosition);
            this.isOnePlayerMode = isOnePlayerMode;
        }

        public void SetPlayers( Paddle playerOne, Paddle playerTwo)
        {
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
        }
        public void SetPlayers( Paddle playerOne)
        {
            this.playerOne = playerOne;
        }

        public void SetPlayerTextBox(TextBox PlayerOne)
        {
            playerOneTextBox = PlayerOne;
        }

        public void SetPlayerTextBox(TextBox PlayerOne, TextBox PlayerTwo)
        {
            playerOneTextBox = PlayerOne;
            playerTwoTextBox = PlayerTwo;
        }
        
        public override bool Move(double windowHeight, double windowWidth)
        {
            CollisionCheck(); //Check if players touch the ball
            
            // Detect Collision with wall, Score for point should check here 
            if (Canvas.GetTop(this.BallItem) < 0 || Canvas.GetTop(this.BallItem) > windowHeight)
            {
                xDirection = -xDirection;
            }

            if (Canvas.GetLeft(this.BallItem) < 0 || Canvas.GetLeft(this.BallItem) > windowWidth)
            {
                if (!isOnePlayerMode){

                    if (Canvas.GetLeft(this.BallItem) > windowWidth)
                    {
                        playerOneTextBox.Text = (++playerOne.Score).ToString();
                        return false;
                    }
                    else
                    {
                        playerTwoTextBox.Text = (++playerTwo.Score).ToString();
                        return false;
                    }
                }
                else if (Canvas.GetLeft(this.BallItem) <= 0)
                {
                    return false;
                }

                //Console.WriteLine($"P1: {playerOne.Score}, P2: {playerTwo.Score}");
                yDirection = -yDirection;
                // nextRound();
            }

            Canvas.SetTop(BallItem, Canvas.GetTop(BallItem) + xDirection * speed);
            Canvas.SetLeft(BallItem, Canvas.GetLeft(BallItem) + yDirection * speed);
            return true;
        }

        private void CollisionCheck()
        {
            CollidePlayer(playerOne);
            if (!isOnePlayerMode) CollidePlayer(playerTwo);

        }

        private void CollidePlayer(Paddle player)
        {
            // This glorious shit really checks if the ball hit a paddle
            if (Canvas.GetTop(player.Rectangle) < Canvas.GetTop(BallItem) 
                && Canvas.GetTop(player.Rectangle) + player.Rectangle.Height > Canvas.GetTop(BallItem)
                && ((int) Canvas.GetLeft(player.Rectangle) == (int) Canvas.GetLeft(BallItem) - (int) BallItem.Width/2
                    || (int) Canvas.GetLeft(player.Rectangle) == (int) Canvas.GetLeft(BallItem) + (int) BallItem.Width/2))
            {
                LastTouchedPlayer = player;
                // Change the direction of the ball
                yDirection = -yDirection;
                // according to the player movement
                xDirection = player.Direction;
                if (isOnePlayerMode) playerOneTextBox.Text = (++playerOne.Score).ToString();
            }
        }
    }
}