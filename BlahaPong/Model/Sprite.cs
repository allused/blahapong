using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BlahaPong.Model
{
    public abstract class Sprite
    {
        protected int speed;

        protected Sprite(int speed)
        {
            this.speed = speed;
        }

        public abstract bool Move(double Height, double Width);
    }
}
