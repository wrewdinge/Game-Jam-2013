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
		ChildHood = 0,
		Teenager,
		Twenties,
		MiddleAge,
		OldAge
	}

	class LevelManager
	{
		private List<Rectangle>	mCollideables;
		private List<Sprite>	mSprites;
		private Rectangle		mGoal;
		private List<Unit>		mUnits;
		public CurrentLevel eCurrLevel;

		public LevelManager()
		{
			mCollideables = new List<Rectangle>();
			mSprites = new List<Sprite>();
			eCurrLevel = CurrentLevel.ChildHood;
			mGoal = Rectangle.Empty;
		}

		public void addObjects()
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
				mUnits.update(gameTime);
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
	}
}
