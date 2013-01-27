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
        Unit p1;
        MenuManager menuManager;
        InputManager input;
        List<Rectangle> CollisionRectangles;
        List<Texture2D> menuTextures;

        SpriteFont bigFont, smallFont;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            input = new InputManager();
            input.Alias("left", Keys.A);
            input.Alias("right", Keys.D);
            input.Alias("up", Keys.W);
            input.Alias("down", Keys.S);
            input.Alias("left", Keys.Left);
            input.Alias("right", Keys.Right);
            input.Alias("up", Keys.Up);
            input.Alias("down", Keys.Down);
            input.Alias("space", Keys.Space);
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
            menuManager = new MenuManager(menuTextures, 5, bigFont, smallFont);
            //menuManager.loadMenu(
            CollisionRectangles = new List<Rectangle>();
            CollisionRectangles.Add(new Rectangle(200, 100, 50, 600));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p1 = new Unit(new Vector2(100, 100), new Vector2(23, 46), 1, 2, 1, 0, 3, 50, Content.Load<Texture2D>("mom"));
            p1.loadRectangleList(CollisionRectangles);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected void playerInput()
        {
            if(InputManager.Down("left"))
            {
                p1.setDirection(Direction.LEFT);
            }
            else if (InputManager.Down("right"))
            {
                p1.setDirection(Direction.RIGHT);
            }
            else if (InputManager.Down("up"))
            {
                p1.setDirection(Direction.UP);
            }
            else if (InputManager.Down("down"))
            {
                p1.setDirection(Direction.DOWN);
            }
            else
            {
                p1.setDirection(Direction.STOP);
            }

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            input.Update(gameTime);
            playerInput();
            // TODO: Add your update logic here
            p1.update(gameTime);

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
            p1.draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
