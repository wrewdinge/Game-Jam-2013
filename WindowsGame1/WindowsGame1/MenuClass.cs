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
    public enum currentMenu
    {
        mainMenu,
        controlsMenu,
        optionsMenu,
        //languageMenu,
        //languageMenu_english,
        //languageMenu_spanish,
        //languageMenu_swedish,
        //languageMenu_german,
        creditMenu,
        pausedMenu,
        //musicMenu,
        //musicOnMenu,
        //musicOffMenu,
        //musicLouderMenu,
        //musicQuieterMenu,
        playGame
    }

    class MenuClass
    {

        private Texture2D mMenuTexture;
        StreamReader mFileRead;
        private Rectangle mMouseRectangle;
     
        private string mTitleString, mText;
        private string mLanguage;

        private List<Rectangle> textRectangles;
        private SpriteFont mTextFont;
        private int clickDelay;
        private const int clickDelayMax = 20;
        private int mNumberOfLines;
        private StringBuilder stringBuilder = new StringBuilder();
        private XboxMouseClass controlMouse;

        public MenuClass(Texture2D menuTexture, Texture2D buttonSprite, SpriteFont textFont)
        {
            mMenuTexture = menuTexture;
            mLanguage = "english";
            mTextFont = textFont;
            clickDelay = clickDelayMax;
            updateMenuScreen(currentMenu.mainMenu);
            mNumberOfLines = 0;
            controlMouse = new XboxMouseClass();
        }

        private int mouseClick()
        {
            for (int i = 0; i < mButtonList.Count; i++)
            {
                if (mMouseRectangle.Intersects(mButtonList[i].returnRectangle()))
                    return i;
            }
            return 99;
        }

        public bool update()
        {
            controlMouse.updateMousePos();
            mMouseRectangle = new Rectangle((int)controlMouse.getMouseX(), (int)controlMouse.getMouseY(), 1, 1);

            if (clickDelay < clickDelayMax)
                clickDelay++;

            if (controlMouse.click() && clickDelay == clickDelayMax)
            {
                int nextMenu = mouseClick();
                if (nextMenu != 99)
                {
                    if (mButtonList[nextMenu].returnMenu() == currentMenu.playGame)
                    {
                        return true;
                    }
                    else
                    {
                        updateMenuScreen(mButtonList[nextMenu].returnMenu());
                    }
                    clickDelay = 0;
                }
            }
            return false;
        }

        private void updateMenuScreen(currentMenu updateToThisMenu)
        {
            switch (updateToThisMenu)
            {
                case currentMenu.mainMenu:
                    {
                        mButtonList.Clear();
                        stringBuilder.Clear();
                        mFileRead = new StreamReader("Content/" + language + "MainMenu.txt");
                        mTitleString = mFileRead.ReadLine();
                        mText = mFileRead.ReadLine();
                        ButtonClass playGameButton = new ButtonClass(new Rectangle(400, 150, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.playGame, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(playGameButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass controlsButton = new ButtonClass(new Rectangle(400, 280, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.controlsMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(controlsButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass optionsButton = new ButtonClass(new Rectangle(400, 410, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.optionsMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(optionsButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass creditsButton = new ButtonClass(new Rectangle(400, 540, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.creditMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(creditsButton);
                        break;
                    }

                /*
           case currentMenu.languageMenu:
               {
                   mButtonList.Clear();
                   stringBuilder.Clear();
                   mFileRead = new StreamReader("Content/" + language + "LanguageMenu.txt");
                   mTitleString = mFileRead.ReadLine();

                   mText = mFileRead.ReadLine();
                   ButtonClass englishButton = new ButtonClass(new Rectangle(400, 150, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.languageMenu_english, mButtonSprite, mText, mButtonFont);
                   mButtonList.Add(englishButton);

                   mText = mFileRead.ReadLine();
                   ButtonClass spanishButton = new ButtonClass(new Rectangle(400, 280, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.languageMenu_spanish, mButtonSprite, mText, mButtonFont);
                   mButtonList.Add(spanishButton);

                   mText = mFileRead.ReadLine();
                   ButtonClass swedishButton = new ButtonClass(new Rectangle(400, 410, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.languageMenu_swedish, mButtonSprite, mText, mButtonFont);
                   mButtonList.Add(swedishButton);

                   mText = mFileRead.ReadLine();
                   ButtonClass germanButton = new ButtonClass(new Rectangle(400, 540, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.languageMenu_german, mButtonSprite, mText, mButtonFont);
                   mButtonList.Add(germanButton);

                   mText = mFileRead.ReadLine();
                   ButtonClass backButton = new ButtonClass(new Rectangle(900, 620, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.optionsMenu, mButtonSprite, mText, mButtonFont);
                   mButtonList.Add(backButton);
                   break;
               }
             
           case currentMenu.languageMenu_english:
               {
                   language = "english";

                   updateMenuScreen(currentMenu.languageMenu);
                   break;
               }
           case currentMenu.languageMenu_spanish:
               {
                   language = "spanish";

                   updateMenuScreen(currentMenu.languageMenu);
                   break;
               }
           case currentMenu.languageMenu_swedish:
               {
                   language = "swedish";

                   updateMenuScreen(currentMenu.languageMenu);
                   break;
               }
           case currentMenu.languageMenu_german:
               {
                   language = "german";

                   updateMenuScreen(currentMenu.languageMenu);
                   break;
               }
               */
                case currentMenu.musicMenu:
                    {
                        mButtonList.Clear();
                        stringBuilder.Clear();
                        mFileRead = new StreamReader("Content/" + language + "MusicMenu.txt");
                        mTitleString = mFileRead.ReadLine();

                        mText = mFileRead.ReadLine();
                        ButtonClass musicOnButton = new ButtonClass(new Rectangle(400, 150, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.musicOnMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(musicOnButton);

                        mText = mFileRead.ReadLine();
                        ButtonClass musicOffButton = new ButtonClass(new Rectangle(400, 280, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.musicOffMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(musicOffButton);

                        mText = mFileRead.ReadLine();
                        ButtonClass musicLouderButton = new ButtonClass(new Rectangle(400, 410, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.musicLouderMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(musicLouderButton);

                        mText = mFileRead.ReadLine();
                        ButtonClass musicQuieterButton = new ButtonClass(new Rectangle(400, 540, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.musicQuieterMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(musicQuieterButton);

                        mText = mFileRead.ReadLine();
                        ButtonClass backButton = new ButtonClass(new Rectangle(900, 620, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.mainMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(backButton);
                        break;
                    }
                case currentMenu.musicOnMenu:
                    {
                        MediaPlayer.IsMuted = false;
                        updateMenuScreen(currentMenu.musicMenu);
                        break;
                    }
                case currentMenu.musicOffMenu:
                    {
                        MediaPlayer.IsMuted = true;
                        updateMenuScreen(currentMenu.musicMenu);
                        break;
                    }
                case currentMenu.musicLouderMenu:
                    {
                        if (MediaPlayer.Volume < 1)
                            MediaPlayer.Volume += .25f;
                        updateMenuScreen(currentMenu.musicMenu);
                        break;
                    }
                case currentMenu.musicQuieterMenu:
                    {
                        if (MediaPlayer.Volume > 0)
                            MediaPlayer.Volume -= .25f;
                        updateMenuScreen(currentMenu.musicMenu);
                        break;
                    }
                case currentMenu.optionsMenu:
                    {
                        mButtonList.Clear();
                        stringBuilder.Clear();
                        mFileRead = new StreamReader("Content/" + language + "OptionsMenu.txt");
                        mTitleString = mFileRead.ReadLine();

                        mText = mFileRead.ReadLine();
                        ButtonClass languageOptionsButton = new ButtonClass(new Rectangle(400, 150, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.languageMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(languageOptionsButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass musicOptionsButton = new ButtonClass(new Rectangle(400, 280, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.musicMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(musicOptionsButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass cheatsButton = new ButtonClass(new Rectangle(400, 410, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.cheatMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(cheatsButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass optionsButton = new ButtonClass(new Rectangle(400, 540, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.difficultyMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(optionsButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass backButton = new ButtonClass(new Rectangle(900, 620, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.mainMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(backButton);

                        break;
                    }

                case currentMenu.pausedMenu:
                    {
                        mButtonList.Clear();
                        stringBuilder.Clear();
                        mFileRead = new StreamReader("Content/" + language + "Paused.txt");
                        mTitleString = mFileRead.ReadLine();
                        mText = mFileRead.ReadLine();
                        ButtonClass continueButton = new ButtonClass(new Rectangle(400, 150, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.playGame, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(continueButton);
                        mText = mFileRead.ReadLine();
                        ButtonClass exitButton = new ButtonClass(new Rectangle(400, 280, buttonRectangleSize.Width, buttonRectangleSize.Height), currentMenu.mainMenu, mButtonSprite, mText, mButtonFont);
                        mButtonList.Add(exitButton);
                        break;
                    }
                default:
                    {
                        mButtonList.Clear();
                        break;
                    }
            }
        }

        public void pause()
        {
            updateMenuScreen(currentMenu.pausedMenu);
        }

        public void draw(SpriteBatch spriteBatch, Texture2D pointer)
        {
            spriteBatch.Draw(mMenuTexture, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(mTitleFont, mTitleString, new Vector2(400, 10), Color.DarkRed);
            if (stringBuilder.Length != 0)
                spriteBatch.DrawString(mButtonFont, stringBuilder.ToString(), new Vector2(100, 150), Color.Black);


            for (int i = 0; i < mButtonList.Count; i++)
            {
                mButtonList[i].draw(spriteBatch, mMouseRectangle);
            }

            spriteBatch.Draw(pointer, new Rectangle(mMouseRectangle.X - 2, mMouseRectangle.Y - 2, 60, 60), Color.White);
        }


    }
}
