using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // declaring window dimensions
        const int WindowWidth = 800;
        const int WindowHeight = 600;
        
        Random rand = new Random();
        Vector2 centerLocation = new Vector2(
            WindowWidth / 2, WindowHeight / 2);

        // declaring three rock sprites
        Texture2D greenRock;
        Texture2D magentaRock;
        Texture2D whiteRock;

        // declaring rock variables
        Rock rock0;
        Rock rock1;
        Rock rock2;

        // delay support
        const int TotalDelayMilliseconds = 1000;
        int elapsedDelayMilliseconds = 0;

        // random velocity support
        const float BaseSpeed = 2f;
        Vector2 upLeft = new Vector2(-BaseSpeed, -BaseSpeed);
        Vector2 upRight = new Vector2(BaseSpeed, -BaseSpeed);
        Vector2 downRight = new Vector2(BaseSpeed, BaseSpeed);
        Vector2 downLeft = new Vector2(-BaseSpeed, BaseSpeed);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // change resolution
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // loading 3 rock sprites
            greenRock = Content.Load<Texture2D>(@"Graphics\greenrock");
            magentaRock = Content.Load<Texture2D>(@"Graphics\magentarock");
            whiteRock = Content.Load<Texture2D>(@"Graphics\whiterock");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // updating rocks if they are deployed
            if (rock0 != null)
            {
                rock0.Update(gameTime);
            }

            if (rock1 != null)
            {
                rock1.Update(gameTime);
            }

            if (rock2 != null)
            {
                rock2.Update(gameTime);
            }

            // update timer
            elapsedDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedDelayMilliseconds >= TotalDelayMilliseconds)
            {
                // spawning new rocks if there is fewer than 3 rocks in window
                if(rock0 == null)
                {
                    rock0 = GetRandomRock();
                }
                else if(rock1 == null)
                {
                    rock1 = GetRandomRock();
                }
                else if(rock2 == null)
                {
                    rock2 = GetRandomRock();
                }

                // restart timer
                elapsedDelayMilliseconds = 0;
            }

            // checking if rock is deployed and it is outside the window, then spawning new rock
            if(rock0 != null && rock0.OutsideWindow)
            {
                rock0 = GetRandomRock();
            }

            else if (rock1 != null && rock1.OutsideWindow)
            {
                rock1 = GetRandomRock();
            }

            else if (rock2 != null && rock2.OutsideWindow)
            {
                rock2 = GetRandomRock();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // drawing rocks if they are deployed
            spriteBatch.Begin();

            if(rock0 != null)
            rock0.Draw(spriteBatch);

            if(rock1 != null)
            rock1.Draw(spriteBatch);

            if(rock2 != null)
            rock2.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets a rock with a random sprite and velocity
        /// </summary>
        /// <returns>the rock</returns>
        private Rock GetRandomRock()
        {
            // calling random sprite method
            Texture2D sprite = GetRandomSprite();

            // calling random velocity method
            Vector2 velocity = GetRandomVelocity();

            // return a new rock, centered in the window, with the random sprite and velocity
            return new Rock(sprite, centerLocation, velocity, WindowWidth, WindowHeight);
        }

        /// <summary>
        /// Gets a random sprite
        /// </summary>
        /// <returns>the sprite</returns>
        private Texture2D GetRandomSprite()
        {
            // generating random sprite for spawning new rock
            int spriteNumber = rand.Next(0, 3);

            if (spriteNumber == 0)
            {
                return greenRock;
            }
            else if (spriteNumber == 1)
            {
                return magentaRock;
            }
            else
            {
                return whiteRock;
            }
        }

        /// <summary>
        /// Gets a random velocity
        /// </summary>
        /// <returns>the velocity</returns>
        private Vector2 GetRandomVelocity()
        {
            // generating random velocity for spawning new rock
            int velocityNumber = rand.Next(0, 4);

            if (velocityNumber == 0)
            {
                return upLeft;
            }
            else if (velocityNumber == 1)
            {
                return upRight;
            }
            else if (velocityNumber == 2)
            {
                return downRight;
            }
            else
            {
                return downLeft;
            }
        }
    }
}
