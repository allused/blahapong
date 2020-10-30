using System;
using System.Collections.Generic;

namespace BlahaPong.Model
{
    public class DoubleSizeGift : Gift
    {
        public DoubleSizeGift(int speed, int height, int width, int xPosition, int yPosition, ref List<Ball> balls) : base(speed, height, width, xPosition, yPosition, ref balls)
        {
        }

        public override void GiftDeactivation()
        {
            player.Rectangle.Height = 100;
            IsActive = false;
        }

        public override void GiftActivation()
        {
            player.Rectangle.Height = 200;
        }

    }
}