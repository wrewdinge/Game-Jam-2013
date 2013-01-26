using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace WindowsGame1
{
    class AI_Unit : Unit
    {
        int mDistance;
        public AI_Unit(Vector2 position, Vector2 size, int speed, int movingUpRowNum, int movingDownRowNum, int movingLeftRightRowNum, int numFramesPerRow, int animationSpeed, Texture2D sprite) 
            : base(position, size, speed, movingUpRowNum, movingDownRowNum, movingLeftRightRowNum, numFramesPerRow, animationSpeed, sprite)
        {
            mDistance = 0;
        }


        public void moveUnit(Direction direction, int distance)
        {
            mDistance = distance;
            setDirection(direction);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            mDistance -= (int)mVelocity.X;
            mDistance -= (int)mVelocity.Y;
            if (mDistance <= 0)
            {
                setDirection(Direction.STOP);
            }
        }

    }
}
