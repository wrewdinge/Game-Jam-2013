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

namespace WindowsGame1
{
	public enum CurrentLevel
	{
		None = 0,
		ChildHood,
		Teenager,
		Twenties,
		MiddleAge,
		OldAge
	}

	class LevelManager
	{
		private List<Rectangle>	mCollideables;
		private List<Sprite>	mSprites;
		private List<Unit>		mUnits;
		private Rectangle		mGoal;
        private Unit mPlayer;
        private Direction mDirection;
		public CurrentLevel eCurrLevel;

        protected void mPlayerInput()
        {
            
            switch (mDirection)
            {
                case Direction.RIGHT:
                    if (InputManager.Up("right") || InputManager.Down("left"))
                    {
                        mDirection = Direction.STOP;
                    }
                    break;
                case Direction.LEFT:
                    if (!InputManager.Down("left") || InputManager.Down("right"))
                    {
                        mDirection = Direction.STOP;
                    }
                    break;
                case Direction.UP:
                    if (!InputManager.Down("up") || InputManager.Down("down"))
                    {
                        mDirection = Direction.STOP;
                    }
                    break;
                case Direction.DOWN:
                    if (!InputManager.Down("down") || InputManager.Down("up"))
                    {
                        mDirection = Direction.STOP;
                    }
                    break;
            }


            if (mDirection == Direction.STOP)
            {
                if (!(InputManager.Down("left") && InputManager.Down("right")))
                {
                    if (InputManager.Down("right") || Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        mDirection = Direction.RIGHT;
                    }
                    else if (InputManager.Down("left"))
                    {
                        mDirection = Direction.LEFT;
                    }

                }
                if (!(InputManager.Down("up") && InputManager.Down("down")))
                {
                    if (InputManager.Down("up"))
                    {
                        mDirection = Direction.UP;
                    }
                    else if (InputManager.Down("down"))
                    {
                        mDirection = Direction.DOWN;
                    }

                }
            }
            checkNewKeyPress();
            mPlayer.setDirection(mDirection);
        }

        protected void checkNewKeyPress()
        {
            if (InputManager.Pressed("up"))
            {
                mDirection = Direction.UP;
            }
            if (InputManager.Pressed("down"))
            {
                mDirection = Direction.DOWN;
            }
            if (InputManager.Pressed("left"))
            {
                mDirection = Direction.LEFT;
            }
            if (InputManager.Pressed("right"))
            {
                mDirection = Direction.RIGHT;
            }
        }

        

		public LevelManager()
		{
			mCollideables = new List<Rectangle>();
			mSprites = new List<Sprite>();
			mUnits = new List<Unit>();
			eCurrLevel = CurrentLevel.ChildHood;
			mGoal = Rectangle.Empty;
		}

		public void nextLevel()
		{
			eCurrLevel++;
			clear();
			addObjects();
		}

		private void addObjects()
		{
			switch (eCurrLevel)
			{
				case CurrentLevel.ChildHood:
					childHood();
					break;

				case CurrentLevel.Teenager:
					teenager();
					break;

				case CurrentLevel.Twenties:
					twenties();
					break;
					
				case CurrentLevel.MiddleAge:
					middleAge();
					break;

				case CurrentLevel.OldAge:
					oldAge();
					break;
			}
		}

		public void update(GameTime gameTime)
		{
			for (int i = 0; i < mUnits.Count; i++)
				mUnits[i].update(gameTime);
		}

		public void draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < mUnits.Count; i++)
				mUnits[i].draw(spriteBatch);
			for (int i = 0; i < mSprites.Count; i++)
				mSprites[i].draw(spriteBatch);
		}

		private void childHood()
		{

		}

		private void teenager()
		{

		}

		private void twenties()
		{

		}

		private void middleAge()
		{

		}

		private void oldAge()
		{

		}

		private void clear()
		{
			mCollideables.Clear();
			mUnits.Clear();
			mSprites.Clear();
			mGoal = Rectangle.Empty;
		}
	}
}
