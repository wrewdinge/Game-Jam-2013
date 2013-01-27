using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MenuManager menuManager;
        LevelManager levelManager;
        List<Rectangle> CollisionRectangles;
        List<Texture2D> menuTextures;

        SpriteFont bigFont, smallFont;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

  
        protected override void Initialize()
        {
            levelManager = new LevelManager();
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            menuTextures = new List<Texture2D>();
            menuTextures.Add(Content.Load<Texture2D>("mainBackground"));
            bigFont = Content.Load<SpriteFont>("bigTextFont");
            smallFont = Content.Load<SpriteFont>("tinyTextFont");
            
            menuManager = new MenuManager(menuTextures, 50, bigFont, smallFont);
            menuManager.loadMainMenu();
            
            levelManager.loadContent(Content);


            CollisionRectangles = new List<Rectangle>();
            CollisionRectangles.Add(new Rectangle(200, 100, 50, 600));
            spriteBatch = new SpriteBatch(GraphicsDevice);

            levelManager.nextLevel();
          
        }

     
        protected override void UnloadContent()
        {
            
        }
      
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (menuManager.getMenuState() == MenuStates.mainMenu)
            {
                this.IsMouseVisible = true;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    menuManager.mouseClick();
                }
            }
            else if (menuManager.getMenuState() == MenuStates.InGame)
            {
                menuManager.getMenuState();
            }

            menuManager.update();
            levelManager.update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            menuManager.draw(spriteBatch);
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
