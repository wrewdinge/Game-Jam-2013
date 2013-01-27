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
        private List<Rectangle> mBoundingBox;
		private List<Sprite>	mSprites;
		private List<Unit>		mUnits;
		private Rectangle		mGoal;
        private Unit			mPlayer;
        private Direction		mDirection;
		private InputManager	mInput;
		public CurrentLevel		eCurrLevel;

        private int StageHeight;
        private int StageWidth;

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

		public LevelManager(int stageWidth, int stageHeight)
		{
			mCollideables = new List<Rectangle>();
            mBoundingBox = new List<Rectangle>();
			mSprites = new List<Sprite>();
			mUnits = new List<Unit>();
			eCurrLevel = CurrentLevel.None;
			mGoal = Rectangle.Empty;

			mInput = new InputManager();
			mInput.Alias("left", Keys.A);
			mInput.Alias("right", Keys.D);
			mInput.Alias("up", Keys.W);
			mInput.Alias("down", Keys.S);
            mInput.Alias("action", Keys.Space);

            StageHeight = stageHeight;
            StageWidth = stageWidth;
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

            child = content.Load<Texture2D>("child");
            teen = content.Load<Texture2D>("teen");
            middle = content.Load<Texture2D>("middle");
            twenty = content.Load<Texture2D>("twenty");
            old = content.Load<Texture2D>("old");

            mother = content.Load<Texture2D>("mom");
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
            checkWin();

            //for (int i = 0; i < mUnits.Count; i++)
            //    mUnits[i].update(gameTime);

            Console.WriteLine(mGoal.ToString());
		}

        private void checkWin()
        {
            if (mPlayer.checkCollide(mGoal) && InputManager.Down("action"))
                nextLevel();
        }

		public void draw(SpriteBatch spriteBatch)
		{
            mSprites[0].draw(spriteBatch);
            mPlayer.draw(spriteBatch);
            for (int i = 2; i < mSprites.Count; i++)
                mSprites[i].draw(spriteBatch);
            mSprites[1].draw(spriteBatch);
			for (int i = 0; i < mUnits.Count; i++)
				mUnits[i].draw(spriteBatch);

            //for (int i = 0; i < mCollideables.Count; i++)
            //    spriteBatch.Draw(mother, new Vector2(mCollideables[i].X, mCollideables[i].Y), new Rectangle(10, 10, 1, 1), Color.Red, 0f, Vector2.Zero, new Vector2(mCollideables[i].Width, mCollideables[i].Height), SpriteEffects.None, 1f);
            
		}

        private void childHood()
		{
            //Sprite Mother = new Sprite(mother, new Vector2(458, 184), new Rectangle(26, 46, 13, 46), Color.White);
            Unit Mother = new Unit(new Vector2(458, 184), new Vector2(13, 46), mother);
            mPlayer = new Unit(new Vector2(550 - 240, 390 - 60), new Vector2(14, 28), child);
            mSprites.Add(new Sprite(level01, new Vector2((StageWidth/2 - 640/2), StageHeight/2 - 360/2), new Rectangle(0, 0, 640, 360), Color.White, 0));
            mSprites.Add(new Sprite(level01Over, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mBoundingBox.Add(new Rectangle(515 - 240, 345 - 60, 260, 55));
            mBoundingBox.Add(new Rectangle(736 - 240, 308 - 60, 10, 70));
            mBoundingBox.Add(new Rectangle(618 - 240, 255 - 60, 160, 58));
            mCollideables.Add(new Rectangle(570 - 240, 343 - 60, 81, 11));
            mCollideables.Add(new Rectangle(712 - 240, 253 - 60, 66, 28));
            mUnits.Add(Mother);
            Mother.mHitBox = new Rectangle(458, 184, 13, 46);
            mCollideables.Add(Mother.getRectangle());
            mPlayer.loadRectangleList(mCollideables, mBoundingBox);

            mGoal = Mother.getRectangle();
           // mGoal.X = Mother;
           // mGoal.Y = Mother.getRectangle().Y - 1;
           // mGoal.Width = Mother.getRectangle().Width + 2;
            //mGoal.Height = Mother.getRectangle().Height + 2;
		}

		private void teenager()
		{
            mPlayer = new Unit(new Vector2(590 - 240, 300 - 60), new Vector2(19, 44), teen);
            mSprites.Add(new Sprite(level02, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mSprites.Add(new Sprite(level02Over, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mBoundingBox.Add(new Rectangle(451 - 240, 230 - 60, 145, 120));
            mBoundingBox.Add(new Rectangle(587 - 240, 348 - 60, 10, 70));
            mBoundingBox.Add(new Rectangle(587 - 240, 380 - 60, 265, 40));
            mCollideables.Add(new Rectangle(454 - 240, 255 - 60, 128, 70));
            mPlayer.loadRectangleList(mCollideables, mBoundingBox);
		}

		private void twenties()
		{
            mPlayer = new Unit(new Vector2(500-240, 336-60), new Vector2(18, 45), twenty);
            mSprites.Add(new Sprite(level03, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mSprites.Add(new Sprite(level03Over, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mBoundingBox.Add(new Rectangle(459-240, 316-60, 352, 60));
            mPlayer.loadRectangleList(mCollideables, mBoundingBox);
		}

		private void middleAge()
		{
            mPlayer = new Unit(new Vector2(500 - 240, 500 - 60), new Vector2(19, 45), middle);
            mSprites.Add(new Sprite(level04, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mSprites.Add(new Sprite(level04Over, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mPlayer.loadRectangleList(mCollideables, mBoundingBox);
		}

		private void oldAge()
		{
            mPlayer = new Unit(new Vector2(564 - 240, 388 - 60), new Vector2(20, 42), old, .3f, 150);
            mSprites.Add(new Sprite(level05, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mSprites.Add(new Sprite(level05Over, new Vector2((StageWidth / 2 - 640 / 2), StageHeight / 2 - 360 / 2), new Rectangle(0, 0, 640, 360), Color.White));
            mBoundingBox.Add(new Rectangle(529 - 240, 321 - 60, 206, 85));
            mCollideables.Add(new Rectangle(450, 328, 54, 25));
            mCollideables.Add(new Rectangle(298, 260, 77, 23));
            mPlayer.loadRectangleList(mCollideables, mBoundingBox);
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
