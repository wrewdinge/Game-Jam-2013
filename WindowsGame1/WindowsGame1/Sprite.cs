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
	class Sprite
	{
		private Texture2D mSpriteSheet;
		private Vector2 mLocation;
		private float mRotation;
		private Rectangle mSheetLocation;
		private Color mColor;
		private Vector2 mOrigin;
		private SpriteEffects mSpriteEffect;
		private float mLayer;
		private float mScale;

		public Sprite()
		{
			mSpriteSheet = null;
			mLocation = Vector2.Zero;
			mRotation = 0;
			mSheetLocation = Rectangle.Empty;
			mColor = Color.White;
			mOrigin = Vector2.Zero;
			mSpriteEffect = SpriteEffects.None;
			mLayer = 0;
		}

		public Sprite(Texture2D spriteSheet, Vector2 location, Rectangle sheetLocation, Color color, float layer = 1, float scale = 1, float rotation = 0, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			mSpriteSheet = spriteSheet;
			mLocation = location;
			mSheetLocation = sheetLocation;
			mColor = color;
			mOrigin = Vector2.Zero;
			mSpriteEffect = spriteEffect;
			mLayer = layer;
			mScale = scale;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(mSpriteSheet, mLocation, mSheetLocation, mColor, mRotation, mOrigin, mScale, mSpriteEffect, mLayer);
            //spriteBatch.Draw(mSpriteSheet, mLocation, new Rectangle(0, 0, 1, 1), Color.Red, 0f, Vector2.Zero, new Vector2(mSheetLocation.Width, mSheetLocation.Height), SpriteEffects.None, 1f); 
		}

		public Rectangle getHitBox()
		{
			Rectangle hitBox = new Rectangle((int)mLocation.X, (int)mLocation.Y, mSheetLocation.Width, mSheetLocation.Height);

			return hitBox;
		}
	}
}
