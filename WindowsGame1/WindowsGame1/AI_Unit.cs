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
        protected List<Direction> mDirections;
        protected List<int> mDistances;
        public AI_Unit(Vector2 position, Vector2 size, int speed, int movingUpRowNum, int movingDownRowNum, int movingLeftRightRowNum, int numFramesPerRow, int animationSpeed, Texture2D sprite) 
            : base(position, size, speed, movingUpRowNum, movingDownRowNum, movingLeftRightRowNum, numFramesPerRow, animationSpeed, sprite)
        {
           // mDistance = 0;
            mDirections = new List<Direction>();
            mDistances = new List<int>();
        }


        public void moveUnit(Direction direction, int distance)
        {
            if (mDistances.Count == 0)
            {
                mDistances.Add(distance);
                mDirections.Add(direction);
                setDirection(direction);
            }
            else
            {
                mDistances.Add(distance);
                mDirections.Add(direction);
            }            
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (mDistances.Count > 0)
            {
                mDistances[0] -= (int)Math.Abs(mVelocity.X);
                mDistances[0] -= (int)Math.Abs(mVelocity.Y);
                if (mDistances[0] <= 0)
                {
                    mDistances.RemoveAt(0);
                    mDirections.RemoveAt(0);
                    if (mDirections.Count > 0)
                    {
                        setDirection(mDirections[0]);
                    }
                }
            }
            else
            {
                if (mIsMoving)
                    mIsMoving = false;
            }
        }
    }
}
