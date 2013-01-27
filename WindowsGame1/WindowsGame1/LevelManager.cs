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
        private Unit			mPlayer;
        private Direction		mDirection;
		private InputManager	mInput;
		public CurrentLevel		eCurrLevel;

        //I hate my life.
        private Texture2D level01;
        private Texture2D level01Over;
        private Texture2D level02;
        private Texture2D level02Over;
        private Texture2D level03;
        private Texture2D level03Over;
        private Texture2D level04;
        private Texture2D level04Over;
        private Texture2D level05;
        private Texture2D level05Over;

        //God make it stop
        private Texture2D child;
        private Texture2D teen;
        private Texture2D twenty;
        private Texture2D middle;
        private Texture2D old;

        //It just keeps going
        private Texture2D mother;
        private Texture2D bully;
        private Texture2D woman;
        private Texture2D son;
        
        //.... Never do this
        private Texture2D shadowPeople;

		public LevelManager()
		{
			mCollideables = new List<Rectangle>();
			mSprites = new List<Sprite>();
			mUnits = new List<Unit>();
			eCurrLevel = CurrentLevel.ChildHood;
			mGoal = Rectangle.Empty;

			mInput = new InputManager();
			mInput.Alias("left", Keys.A);
			mInput.Alias("right", Keys.D);
			mInput.Alias("up", Keys.W);
			mInput.Alias("down", Keys.S);
		}

        public void loadContent(ContentManager content)
        {
            level01 = content.Load<Texture2D>("Level_001");
            level01Over = content.Load<Texture2D>("Level_001_overlay");
            level02 = content.Load<Texture2D>("Level_002");
            level02Over = content.Load<Texture2D>("Level_002_overlay");
            level03 = content.Load<Texture2D>("Level_003");
            level03Over = content.Load<Texture2D>("Level_003_overlay");
            level04 = content.Load<Texture2D>("Level_004");
            level04Over = content.Load<Texture2D>("Level_004_overlay");
            level05 = content.Load<Texture2D>("Room_5");
            level05Over = content.Load<Texture2D>("Room_5_Overlay");

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
			mInput.Update(gameTime);
			PlayerInput();
			mPlayer.update(gameTime);

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

		protected void PlayerInput()
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
	}
}
