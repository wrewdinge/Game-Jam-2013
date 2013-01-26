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
    public class ButtonClass
    {
        private currentMenu mMenu;
        private Rectangle mRectangle;
        private Texture2D mButtonSprite;
        private string mText;
        private SpriteFont mTextFont;
        

        public ButtonClass(Rectangle position, currentMenu menu, Texture2D sprite, string text, SpriteFont font)
        {
            mButtonSprite = sprite;
            mRectangle = position;
            mMenu = menu;
            mText = text;
            mTextFont = font;
        }

        public currentMenu returnMenu()
        {
            return mMenu;
        }

        public Rectangle returnRectangle()
        {
            return mRectangle;
        }

        public void draw(SpriteBatch spritebatch, Rectangle mousePos)
        {
            spritebatch.Draw(mButtonSprite, mRectangle, Color.White);
            if (mousePos.Intersects(mRectangle))
                spritebatch.DrawString(mTextFont, mText, new Vector2(mRectangle.X + (mRectangle.Width * .1f), mRectangle.Y + (mRectangle.Height * .3f)), Color.White);
            else
                spritebatch.DrawString(mTextFont, mText, new Vector2(mRectangle.X + (mRectangle.Width * .1f), mRectangle.Y + (mRectangle.Height * .3f)), Color.Red);

            
        }
    }
}
