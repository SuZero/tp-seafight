using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Net;

namespace seafight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //global variables
        private Texture2D orb;
        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D pauseButton;
        private Texture2D loadButton;
        private Texture2D loadingScreen;
        private Vector2 orbPosition;
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private Vector2 loadButtonPosition;
        private const float OrbWidth = 50f;
        private const float OrbHeight = 50f;
        private float speed = 1.5f;
        private Thread backgroundThread;
        private bool isLoading = false;
        MouseState mouseState;
        MouseState previousMouseState;
        GameState gameState;


        enum GameState
        {
            StartMenu,
            Loading,
            Playing,
            Paused
        }






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
            //enable the mousepointer
            IsMouseVisible = true;


            startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 150);
            loadButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);

            //set the gamestate to start menu
            gameState = GameState.StartMenu;



            //get the mouse state
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;


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
            // myTexture = Content.Load<Texture2D>("mytexture");
            // TODO: use this.Content to load your game content here

            startButton = Content.Load<Texture2D>("start");
            loadButton = Content.Load<Texture2D>("start");
            exitButton = Content.Load<Texture2D>("exit");
            loadingScreen = Content.Load<Texture2D>("loading");


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //load the game when needed
            //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            if (gameState == GameState.Loading && !isLoading)
            {
                //set backgroundthread
                backgroundThread = new Thread(LoadGame);
                isLoading = true;

                //start backgroundthread
                backgroundThread.Start();
            }

            //move the orb if the game is in progress
            if (gameState == GameState.Playing)
            {
                //move the orb
                orbPosition.X += speed;

                //prevent out of bounds
                if (orbPosition.X > (GraphicsDevice.Viewport.Width - OrbWidth) || orbPosition.X < 0)
                {
                    speed *= -1;
                }
            }

            //wait for mouseclick
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;

            if (gameState == GameState.Playing && isLoading)
            {
                LoadGame();
                isLoading = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();



            if (gameState == GameState.StartMenu)
            {
                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(loadButton, loadButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            }
            if (gameState == GameState.Playing)
            {
                //orb
                spriteBatch.Draw(orb, orbPosition, Color.White);
            }

            if (gameState == GameState.Loading)
            {
                spriteBatch.Draw(loadingScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) -
                           (loadingScreen.Width / 2), (GraphicsDevice.Viewport.Height / 2) -
                           (loadingScreen.Height / 2)), Color.YellowGreen);
            }





            spriteBatch.End();

            base.Draw(gameTime);
        }


        void LoadGame()
        {
            //load the game images into the content pipeline
            orb = Content.Load<Texture2D>("ball");

            //set the position of the orb in the middle of the gamewindow
            orbPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - (OrbWidth / 2),
                             (GraphicsDevice.Viewport.Height / 2) - (OrbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(3000);

            //start playing
            gameState = GameState.Playing;
            isLoading = true;
        }

        void MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);

            //check the startmenu
            if (gameState == GameState.StartMenu)
            {
                Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                                            (int)startButtonPosition.Y, 100, 20);
                Rectangle loadButtonRect = new Rectangle((int)loadButtonPosition.X,
                                           (int)loadButtonPosition.Y, 100, 20);
                Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                            (int)exitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
                {
                   // gameState = GameState.Loading;
                    gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(loadButtonRect)) //player clicked start button
                {
                    gameState = GameState.Loading;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                else if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                {
                    Exit();
                }
            }
        }
    }
}
