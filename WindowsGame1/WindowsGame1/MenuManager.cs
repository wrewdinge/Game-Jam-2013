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

public enum MenuStates
{
    mainMenu,
    Controls,
    Credits,
    Story,
    Options,
    InGame,
    Paused,
    Win,
    Lose
}

namespace WindowsGame1
{
    class MenuManager
    {
        protected List<Texture2D> mMenus;
        protected int mCurrentMenu;
        protected bool mFading;
        protected int mFadeIncrement;
        protected Color mCurrentMenuColor;
        protected Color mNextMenuColor;
        protected SpriteFont mFont;
        protected List<String> mStrings;
        protected List<Vector2> mStringPositions;
        protected List<SpriteFont> mSpriteFonts;
        protected SpriteFont mBigFont;
        protected SpriteFont mSmallFont;
        protected string mMainMenuName;
        protected string mControlsName;
        protected string mCreditsName;
        protected string mOptionsName;
        protected string mStoryName;
        protected List<bool> mIsClickable;
        protected List<string> mGoToString;
        protected int mMouseAtThisIndex;
        protected MenuStates mCurrentMenuStates;
        protected string mPausedName;

        public MenuManager(List<Texture2D> menus, int fadeIncrement, SpriteFont bigFont, SpriteFont smallFont)
        {
            mMainMenuName = "mainMenu";
            mOptionsName = "Options";
            mStoryName = "Story";
            mControlsName = "Controls";
            mCreditsName = "Credits";
            mPausedName = "Paused";
            mCurrentMenuStates = MenuStates.mainMenu;
            mMouseAtThisIndex = -1;
            mMenus = menus;
            mCurrentMenu = 0;
            mFading = false;
            mFadeIncrement = fadeIncrement;
            mCurrentMenuColor = new Color(255, 255, 255, 1f);
            mNextMenuColor = new Color(255, 255, 255, 0f);
            mFont = smallFont;
            mBigFont = bigFont;
            mSmallFont = smallFont;
            mStringPositions = new List<Vector2>();
            mSpriteFonts = new List<SpriteFont>();
            mStrings = new List<string>();
            mGoToString = new List<string>();
            mIsClickable = new List<bool>();
        }

        public void turnOnMenu()
        {
            mCurrentMenuColor.A = 255; 
        }

        public MenuStates getMenuState()
        {
            return mCurrentMenuStates;
        }

        public void loadMainMenu()
        {
            loadMenu(mMainMenuName);
            mCurrentMenuStates = MenuStates.mainMenu;
        }

        public void loadControls()
        {
            loadMenu(mControlsName);
            mCurrentMenuStates = MenuStates.Controls;
        }

        public void loadCredits()
        {
            loadMenu(mCreditsName);
            mCurrentMenuStates = MenuStates.Credits;
        }

        public void loadStory()
        {
            loadMenu(mStoryName);
            mCurrentMenuStates = MenuStates.Story;
        }

        public void loadOptions()
        {
            loadMenu(mOptionsName);
            mCurrentMenuStates = MenuStates.Options;
        }

        public void loadPaused()
        {
            loadMenu(mPausedName);
            mCurrentMenu = 0;
            mCurrentMenuStates = MenuStates.Paused;
        }

        public void loadWin()
        {
            loadMenu("Win");
            mCurrentMenu = 0;
            mCurrentMenuStates = MenuStates.Win;
        }

        public void loadLose()
        {
            loadMenu("Lose");
            mCurrentMenu = 0;
            mCurrentMenuStates = MenuStates.Lose;
        }

        public void clearScreen()
        {
            mStringPositions.Clear();
            mSpriteFonts.Clear();
            mStrings.Clear();
            mGoToString.Clear();
            mIsClickable.Clear();
        }

        public void updateHoveColor()
        {
            bool mouseIsOverAString = false;
            for (int index = 0; index < mStringPositions.Count; index++)
            {
                if (index < mIsClickable.Count && mIsClickable[index])
                {
                    
                    if (Mouse.GetState().Y >= mStringPositions[index].Y && Mouse.GetState().Y <= mStringPositions[index].Y + mSpriteFonts[index].MeasureString(mStrings[index]).Y
                        && Mouse.GetState().X >= mStringPositions[index].X && Mouse.GetState().X <= mStringPositions[index].X + mSpriteFonts[index].MeasureString(mStrings[index]).X)
                    {
                        mMouseAtThisIndex = index;
                        mouseIsOverAString = true;
                    }
                    
                }
            }

            if (!mouseIsOverAString)
            {
                mMouseAtThisIndex = -1;
            }
        }

        public void mouseClick()
        {
            if (mMouseAtThisIndex > 0 && mMouseAtThisIndex < mGoToString.Count)
            {
                switch (mGoToString[mMouseAtThisIndex])
                {
                    case "mainMenu":
                        loadMainMenu();
                        break;
                    case "Controls":
                        loadControls();
                        break;
                    case "Credits":
                        loadCredits();
                        break;
                    case "Story":
                        loadStory();
                        break;
                    case "Options":
                        loadOptions();
                        break;
                    case "Play":
                        mCurrentMenu++;
                        mCurrentMenuStates = MenuStates.InGame;
                        break;
                }
                switch (mCurrentMenuStates)
                {
                    case MenuStates.mainMenu:
                        loadMainMenu();
                        break;
                    case MenuStates.Controls:
                        loadControls();
                        break;
                    case MenuStates.Credits:
                        loadCredits();
                        break;
                    case MenuStates.Options:
                        loadOptions();
                        break;
                    case MenuStates.Story:
                        loadStory();
                        break;
                }
            }
        }

        public void loadMenu(string fileName)
        {
            mMouseAtThisIndex = -1;
            fileName = "Content/" + fileName + ".txt";
            StreamReader inputFile = new StreamReader(fileName);

            string line = inputFile.ReadLine();
            Vector2 tempPosition = Vector2.Zero;
            string currentSpriteFont = "";
            clearScreen();

            while (line != null)
            {

                tempPosition.X = Convert.ToInt32(line);
                line = inputFile.ReadLine();
                tempPosition.Y = Convert.ToInt32(line);
                mStringPositions.Add(tempPosition);
                line = inputFile.ReadLine();
                currentSpriteFont = line;
                switch (currentSpriteFont)
                {
                    case "big":
                        mSpriteFonts.Add(mBigFont);
                        break;
                    case "medium":
                        mSpriteFonts.Add(mFont);
                        break;
                    case "small":
                        mSpriteFonts.Add(mSmallFont);
                        break;
                }
                mStrings.Add(inputFile.ReadLine());
                line = inputFile.ReadLine();
                switch (line)
                {
                    case "no":
                        mIsClickable.Add(false);
                        break;
                    case "yes":
                        mIsClickable.Add(true);
                        break;
                }

                mGoToString.Add(inputFile.ReadLine());
                line = inputFile.ReadLine();
            }
            inputFile.Close();
        }

        private void fade()
        {
            if (mCurrentMenu + 1 < mMenus.Count)
            {
                if (mNextMenuColor.A < 255)
                {
                    //mCurrentMenuColor.A -= (byte)(mFadeIncrement * 255);
                    for (int i = 0; i < mFadeIncrement && mNextMenuColor.A < 255; i++)
                    {
                        mNextMenuColor.A++;
                    }
                    
                }
                else
                {
                    mCurrentMenuColor = new Color(255, 255, 255, 1f);
                    mNextMenuColor = new Color(255, 255, 255, 0f);
                    mCurrentMenu++;
                    mFading = false;
                }
            }
            else
            {
                if (mCurrentMenuColor.A > 0)
                {
                    for (int i = 0; i < mFadeIncrement && mCurrentMenuColor.A > 0; i++)
                    {
                        mCurrentMenuColor.A--;
                    }
                    
                    //mNextMenuColor.A++;
                }
                else
                {
                    mCurrentMenuColor = new Color(255, 255, 255, 1f);
                    mNextMenuColor = new Color(255, 255, 255, 0f);
                    mCurrentMenu++;
                    mFading = false;
                }
            }
        }

        public void update()
        {
            if (mFading)
            {
                fade();
            }

            updateHoveColor();
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (mCurrentMenu < mMenus.Count && mCurrentMenuColor.A > 0)
            {
                spriteBatch.Draw(mMenus[mCurrentMenu], new Rectangle(0, 0, 1280, 768), mCurrentMenuColor);
                for(int index = 0; index < mSpriteFonts.Count && index < mStringPositions.Count && index < mStrings.Count; index++)
                {
                    if(index != mMouseAtThisIndex)
                        spriteBatch.DrawString(mSpriteFonts[index], mStrings[index], mStringPositions[index], Color.Black); //* mCurrentMenuColor.A
                    else
                        spriteBatch.DrawString(mSpriteFonts[index], mStrings[index], mStringPositions[index], Color.White);
                }
            }
            if (mCurrentMenu + 1 < mMenus.Count && mFading)
            {
                spriteBatch.Draw(mMenus[mCurrentMenu + 1], new Rectangle(0, 0, 1280, 768), mNextMenuColor);
            }
        }

    }
}