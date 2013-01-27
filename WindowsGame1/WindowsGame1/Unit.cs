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

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        STOP
    }

	public class Unit
	{

        protected Vector2 mPosition;
        protected Vector2 mSize;
        protected Vector2 mVelocity;
        protected Texture2D mSprite;

        protected int mXFrame;
        protected int mYFrame;
        protected int mNumXFrames;
        protected int mAnimationSpeed;
        protected float mMsUnitilNextFrame;
        protected int mMovingUpRowNum;
        protected int mMovingDownRowNum;
        protected int mMovingLeftRightRowNum;
        protected Direction mDirection;
        protected bool mIsMoving;
        protected int mSpeed;
        protected List<Rectangle> mCollisionRectangles;
        protected Rectangle mHitBox;

        public Unit(Vector2 position, Vector2 size, int speed, int movingUpRowNum, int movingDownRowNum, int movingLeftRightRowNum, int numFramesPerRow, int animationSpeed, Texture2D sprite)
        {
            mPosition = position;
            mVelocity = Vector2.Zero;
            mSize = size;
            mSpeed = speed;
            mXFrame = 0;
            mYFrame = 0;
            mAnimationSpeed = animationSpeed;
            mMsUnitilNextFrame = mAnimationSpeed;
            mMovingUpRowNum = movingUpRowNum;
            mMovingDownRowNum = movingDownRowNum;
            mMovingLeftRightRowNum = movingLeftRightRowNum;
            mSprite = sprite;
            mNumXFrames = numFramesPerRow;
            mIsMoving = false;
            mCollisionRectangles = new List<Rectangle>();
            mDirection = Direction.RIGHT;
        }

        public void changeSprite(Texture2D sprite)
        {
            mSprite = sprite;
        }

        public void changeSprite(Texture2D sprite, Vector2 size)
        {
            mSprite = sprite;
            mSize = size;
        }

        public void changeSprite(Texture2D sprite, Vector2 size, int movingUpRowNum, int movingDownRowNum, int movingLeftRightRowNum, int numFramesPerRow = 3)
        {
            mSprite = sprite;
            mSize = size;
            mMovingUpRowNum = movingUpRowNum;
            mMovingDownRowNum = movingDownRowNum;
            mMovingLeftRightRowNum = movingLeftRightRowNum;
            mNumXFrames = numFramesPerRow;
        }

        public void setDirection(Direction direction)
        {
            if (direction != Direction.STOP)
            {
                mDirection = direction;
                mIsMoving = true;
            }
            else
                mIsMoving = false;
        }

       virtual  public void update(GameTime gameTime)
        {
            if (mIsMoving)
            {
                move();
                if(mIsMoving)
                 animate(gameTime);
            }
            else
            {
                mVelocity = Vector2.Zero;
                mXFrame = 1;
            }

        }

        public void loadRectangleList(List<Rectangle> rectangle)
        {
            mCollisionRectangles = rectangle;
        }

        protected void move()
        {
            switch (mDirection)
            {
                case Direction.RIGHT:
                    mVelocity.X = mSpeed;
                    mVelocity.Y = 0;
                    break;
                case Direction.LEFT:
                    mVelocity.X = -mSpeed;
                    mVelocity.Y = 0;
                    break;
                case Direction.UP:
                    mVelocity.X = 0;
                    mVelocity.Y = -mSpeed;
                    break;
                case Direction.DOWN:
                    mVelocity.X = 0;
                    mVelocity.Y = mSpeed;
                    break;
                default:
                    mVelocity.X = 0;
                    mVelocity.Y = 0;
                    break;
            }


            mHitBox.X = (int)mPosition.X + (int)mVelocity.X;
            mHitBox.Y = (int)mPosition.Y + (int)mVelocity.Y;
            if (canMove())
            {
                mPosition += mVelocity;
            }
            else
            {
                mXFrame = 1;
                mIsMoving = false;
            }
         }

        protected bool canMove()
        {
            for (int index = 0; index < mCollisionRectangles.Count; index++)
            {
                if(mHitBox.Intersects(mCollisionRectangles[index]))
                    return false;
            }
            return true;
        }

        protected void animate(GameTime gameTime)
        {
            mMsUnitilNextFrame -= gameTime.ElapsedGameTime.Milliseconds;

            if (mMsUnitilNextFrame <= 0)
            {
                mXFrame++;
                if (!(mXFrame < mNumXFrames))
                {
                    mXFrame = 0;
                }
                mMsUnitilNextFrame = mAnimationSpeed;
            }
        }

        public bool checkCollide(Rectangle rectangle)
        {
            return mHitBox.Intersects(rectangle);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            switch (mDirection)
            {
                case Direction.RIGHT:
                    flip = SpriteEffects.FlipHorizontally;
                    mYFrame = mMovingLeftRightRowNum;
                    break;
                case Direction.LEFT:
                    flip = SpriteEffects.None;
                    mYFrame = mMovingLeftRightRowNum;
                    break;
                case Direction.UP:
                    flip = SpriteEffects.None;
                    mYFrame = mMovingUpRowNum;
                    break;
                case Direction.DOWN:
                    flip = SpriteEffects.None;
                    mYFrame = mMovingDownRowNum;
                    break;
                default:
                    flip = SpriteEffects.None;
                    mYFrame = 0;
                    break;
            }

            spriteBatch.Draw(mSprite, mPosition, new Rectangle( (int)(mSize.X * mXFrame), (int)(mSize.Y * mYFrame), (int)mSize.X, (int)mSize.Y), Color.White, 0.0f, Vector2.Zero, 1.0f, flip, 1.0f);

        }
        
	}
}
