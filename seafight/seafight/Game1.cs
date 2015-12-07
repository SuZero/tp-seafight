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
        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D loadButton;
        private Texture2D settingsButton;
        private Texture2D aboutButton;
        private Texture2D helpButton;
        private Texture2D backButton;
        private Texture2D easyDifficultyButton;
        private Texture2D normalDifficultyButton;
        private Texture2D hardDifficultyButton;
        private Texture2D onButton;
        private Texture2D offButton;
        private Texture2D lessButton;
        private Texture2D moreButton;
        private Texture2D toBattleButton;
        private Texture2D localgameButton;
        private Texture2D networkgameButton;
        private Texture2D cell;

        private Texture2D loadingScreen;
        private Texture2D startScreen;
        private Texture2D title;
        private Texture2D playScreen;


        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private Vector2 loadButtonPosition;
        private Vector2 settingsButtonPosition;
        private Vector2 aboutButtonPosition;
        private Vector2 helpButtonPosition;
        private Vector2 backButtonPosition;
        private Vector2 easyDifficultyButtonPosition;
        private Vector2 normalDifficultyButtonPosition;
        private Vector2 hardDifficultyButtonPosition;
        private Vector2 onButtonPosition;
        private Vector2 offButtonPosition;
        private Vector2 lessButtonPosition;
        private Vector2 moreButtonPosition;
        private Vector2 toBattleButtonPosition;
        private Vector2 localgameButtonPosition;
        private Vector2 networkgameButtonPosition;
        private Vector2 fieldPosition;
        private Vector2 field2Position;

        private Vector2 startScreenPosition;
        private Vector2 titlePosition;

        private float volume = 1.0f;
        private Thread backgroundThread;
        private bool isLoading = false;
        MouseState mouseState;
        MouseState previousMouseState;
        GameState gameState;
        SoundState soundState;
        ShuffleState shuffleState;
        PlacingState placingState;
        DifficultyState difficultyState;
        Song maintheme;

        enum GameState
        {
            StartMenu,
            GameTypeMenu,
            Settings,
            Loading,
            BattleSettings,
            BattlePrep,
            Playing,
            Paused
        }
        enum SoundState
        {
            On,
            Off
            
        }
        enum ShuffleState
        {
            On,
            Off

        }
        enum PlacingState
        {
            On,
            Off

        }
        enum DifficultyState
        {
            Easy,
            Normal,
            Hard
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


            startButtonPosition = new Vector2(50, 350);
            loadButtonPosition = new Vector2(50, 375);
            settingsButtonPosition = new Vector2(50, 400);
            exitButtonPosition = new Vector2(50, 425);
            aboutButtonPosition = new Vector2(GraphicsDevice.Viewport.Width - 147, 400);
            helpButtonPosition = new Vector2(GraphicsDevice.Viewport.Width - 147, 425);
            backButtonPosition = new Vector2(50, 425);
            easyDifficultyButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 160, 225);
            normalDifficultyButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 225);
            hardDifficultyButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 60, 225);
            localgameButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 200, 100);
            networkgameButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 200, 225);
            onButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 105, 100);
            offButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 5, 100);
            lessButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 25, 150);
            moreButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 5, 150);
            toBattleButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 180);
            startScreenPosition = new Vector2(0, 0);
            titlePosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 200, 150);
            fieldPosition = new Vector2(50, 50);
            field2Position = new Vector2(GraphicsDevice.Viewport.Width - 80, GraphicsDevice.Viewport.Width-80);

            //set the gamestate to start menu
            gameState = GameState.StartMenu;
            soundState = SoundState.On;
            difficultyState = DifficultyState.Normal;
            shuffleState = ShuffleState.Off;
            placingState = PlacingState.On;


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
            localgameButton = Content.Load<Texture2D>("localgame");
            loadButton = Content.Load<Texture2D>("load");
            settingsButton = Content.Load<Texture2D>("settings");
            aboutButton = Content.Load<Texture2D>("about");
            helpButton = Content.Load<Texture2D>("help");
            exitButton = Content.Load<Texture2D>("exit");
            backButton = Content.Load<Texture2D>("back");
            easyDifficultyButton = Content.Load<Texture2D>("easy");
            normalDifficultyButton = Content.Load<Texture2D>("normal");
            hardDifficultyButton = Content.Load<Texture2D>("hard");
            onButton = Content.Load<Texture2D>("on");
            offButton = Content.Load<Texture2D>("off");
            lessButton = Content.Load<Texture2D>("less");
            moreButton = Content.Load<Texture2D>("more");
            toBattleButton = Content.Load<Texture2D>("tobattle");
            networkgameButton = Content.Load<Texture2D>("networkgame");
            loadingScreen = Content.Load<Texture2D>("loading");
            startScreen = Content.Load<Texture2D>("startscreen");
            title = Content.Load<Texture2D>("title");
            cell = Content.Load<Texture2D>("cell");
            playScreen = Content.Load<Texture2D>("map");
            maintheme = Content.Load<Song>("titlemusic");
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
                
            }
            if ((soundState == SoundState.On)&&(MediaPlayer.State==MediaState.Stopped))
            {
                MediaPlayer.Play(maintheme);
                MediaPlayer.Volume = volume;
            }
            if ((soundState == SoundState.Off) && (MediaPlayer.State==MediaState.Playing))
            {
                MediaPlayer.Stop();
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
                spriteBatch.Draw(startScreen,startScreenPosition,Color.White);
                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(loadButton, loadButtonPosition, Color.White);
                spriteBatch.Draw(settingsButton, settingsButtonPosition, Color.White);
                spriteBatch.Draw(aboutButton, aboutButtonPosition, Color.White);
                spriteBatch.Draw(helpButton, helpButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
                spriteBatch.Draw(title, titlePosition, Color.White);
            }
            if (gameState == GameState.GameTypeMenu)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(localgameButton, localgameButtonPosition,Color.White);
                spriteBatch.Draw(networkgameButton, networkgameButtonPosition, Color.White);
                spriteBatch.Draw(backButton, backButtonPosition, Color.White);
            }
            if (gameState == GameState.Settings)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(easyDifficultyButton, easyDifficultyButtonPosition, Color.White);
                spriteBatch.Draw(normalDifficultyButton, normalDifficultyButtonPosition, Color.White);
                spriteBatch.Draw(hardDifficultyButton, hardDifficultyButtonPosition, Color.White);
                spriteBatch.Draw(onButton, onButtonPosition, Color.White);
                spriteBatch.Draw(offButton, offButtonPosition, Color.White);
                spriteBatch.Draw(lessButton, lessButtonPosition, Color.White);
                spriteBatch.Draw(moreButton, moreButtonPosition, Color.White);
                spriteBatch.Draw(backButton, backButtonPosition, Color.White);
            }
            if (gameState == GameState.BattleSettings)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(onButton, onButtonPosition, Color.White);
                spriteBatch.Draw(offButton, offButtonPosition, Color.White);
                spriteBatch.Draw(onButton, new Vector2 (onButtonPosition.X,onButtonPosition.Y+30), Color.White);
                spriteBatch.Draw(offButton, new Vector2(offButtonPosition.X, offButtonPosition.Y + 30), Color.White);
                spriteBatch.Draw(toBattleButton,toBattleButtonPosition, Color.White);
                spriteBatch.Draw(backButton, backButtonPosition, Color.White);
            }
            if (gameState == GameState.Playing)
            {
                spriteBatch.Draw(playScreen, startScreenPosition, null, Color.White, 0, new Vector2(0, 0), (0.6f),0, 1);
                //orb
                if (difficultyState == DifficultyState.Easy)
                {

                    spriteBatch.Draw(easyDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);
                   
                }
                if (difficultyState == DifficultyState.Normal)
                {
                    spriteBatch.Draw(normalDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);
                }
                if (difficultyState == DifficultyState.Hard)
                {
                    spriteBatch.Draw(hardDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);
                }
               
                   

                spriteBatch.Draw(backButton, backButtonPosition, Color.White);

                for (int i=0;i<9;i++)
                    for (int j=0;j<9;j++)
                    {
                        spriteBatch.Draw(cell, new Vector2(fieldPosition.X + i * 30, fieldPosition.Y + j * 30),Color.White);
                        spriteBatch.Draw(cell, new Vector2(field2Position.X - i * 30, fieldPosition.Y + j * 30), Color.White);
                    
                    }
                spriteBatch.Draw(onButton, aboutButtonPosition, Color.White);
                spriteBatch.Draw(offButton, helpButtonPosition, Color.White);
                


               // spriteBatch.Draw(orb, orbPosition, Color.White);
            }

            if (gameState == GameState.BattleSettings)
            {

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
          

            //set the position of the orb in the middle of the gamewindow
            

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
                Rectangle settingsButtonRect = new Rectangle((int)settingsButtonPosition.X,
                                           (int)settingsButtonPosition.Y, 100, 20);
                Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                            (int)exitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
                {
                   // gameState = GameState.Loading;
                    gameState = GameState.GameTypeMenu;
                    Thread.Sleep(500);
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                else if (mouseClickRect.Intersects(loadButtonRect)) //player clicked start button
                {
                    gameState = GameState.Loading;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                else if (mouseClickRect.Intersects(settingsButtonRect)) //player clicked start button
                {
                    gameState = GameState.Settings;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                else if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                {
                    Exit();
                }
            }
            if (gameState == GameState.GameTypeMenu)
            {
                Rectangle localgameButtonRect = new Rectangle((int)localgameButtonPosition.X,
                                            (int)localgameButtonPosition.Y, 400, 100);
                Rectangle networkgameButtonRect = new Rectangle((int)networkgameButtonPosition.X,
                                            (int)networkgameButtonPosition.Y, 400, 100);
                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(localgameButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    gameState = GameState.BattleSettings;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(networkgameButtonRect)) //player clicked start button
                {
                    gameState = GameState.Loading;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                   // gameState = GameState.Playing;
                    gameState = GameState.StartMenu;
                    // isLoading = true;
                }
               
            }
            if (gameState == GameState.Playing)
            {
                
                Rectangle onButtonRect = new Rectangle((int)aboutButtonPosition.X,
                                           (int)aboutButtonPosition.Y, 100, 20);
                Rectangle offButtonRect = new Rectangle((int)helpButtonPosition.X,
                                            (int)helpButtonPosition.Y, 100, 20);

                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(onButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;

                    soundState = SoundState.On;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(offButtonRect)) //player clicked start button
                {
                    soundState = SoundState.Off;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }


                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    gameState = GameState.StartMenu;
                    // isLoading = true;
                }

            }
            if (gameState == GameState.BattleSettings)
            {

                Rectangle shuffleOnButtonRect = new Rectangle((int)onButtonPosition.X,
                                           (int)onButtonPosition.Y, 100, 20);
                Rectangle shuffleOffButtonRect = new Rectangle((int)offButtonPosition.X,
                                            (int)offButtonPosition.Y, 100, 20);
                Rectangle placingOnButtonRect = new Rectangle((int)onButtonPosition.X,
                                           (int)onButtonPosition.Y+30, 100, 20);
                Rectangle placingOffButtonRect = new Rectangle((int)offButtonPosition.X,
                                            (int)offButtonPosition.Y+30, 100, 20);
                Rectangle toBattleButtonRect = new Rectangle((int)toBattleButtonPosition.X,
                                           (int)toBattleButtonPosition.Y, 100, 20);

                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(shuffleOnButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;

                    shuffleState = ShuffleState.On;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(shuffleOffButtonRect)) //player clicked start button
                {
                    shuffleState = ShuffleState.Off;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(placingOnButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;

                    placingState = PlacingState.On;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(placingOffButtonRect)) //player clicked start button
                {
                    placingState = PlacingState.Off;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(toBattleButtonRect)) //player clicked start button
                {
                    if (placingState == PlacingState.On)
                    {
                        // auto-placing method call
                        gameState = GameState.Playing;
                    }
                    else gameState = GameState.BattlePrep;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }

                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    gameState = GameState.StartMenu;
                    // isLoading = true;
                }

            }
            if (gameState == GameState.Settings)
            {
                Rectangle easyDifficultyButtonRect = new Rectangle((int)easyDifficultyButtonPosition.X,
                                            (int)easyDifficultyButtonPosition.Y, 100, 20);
                Rectangle normalDifficultyButtonRect = new Rectangle((int)normalDifficultyButtonPosition.X,
                                            (int)normalDifficultyButtonPosition.Y, 100, 20);
                Rectangle hardDifficultyButtonRect = new Rectangle((int)hardDifficultyButtonPosition.X,
                                            (int)hardDifficultyButtonPosition.Y, 100, 20);
                Rectangle onButtonRect = new Rectangle((int)onButtonPosition.X,
                                            (int)onButtonPosition.Y, 100, 20);
                Rectangle offButtonRect = new Rectangle((int)offButtonPosition.X,
                                            (int)offButtonPosition.Y, 100, 20);
                Rectangle lessButtonRect = new Rectangle((int)lessButtonPosition.X,
                                           (int)lessButtonPosition.Y, 20, 20);
                Rectangle moreButtonRect = new Rectangle((int)moreButtonPosition.X,
                                            (int)moreButtonPosition.Y, 20, 20);
                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(easyDifficultyButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;

                    difficultyState = DifficultyState.Easy;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(normalDifficultyButtonRect)) //player clicked start button
                {
                    difficultyState = DifficultyState.Normal;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(hardDifficultyButtonRect)) //player clicked start button
                {
                    difficultyState = DifficultyState.Hard;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(onButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;

                    soundState = SoundState.On;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(offButtonRect)) //player clicked start button
                {
                    soundState = SoundState.Off;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(lessButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    if (volume>0.0f)
                    {
                    volume = volume-0.1f;
                    MediaPlayer.Volume = volume;
                    }
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(moreButtonRect)) //player clicked start button
                {
                    if (volume < 1.0f)
                    {
                        volume = volume + 0.1f;
                        MediaPlayer.Volume = volume;
                    }
                }
                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    gameState = GameState.StartMenu;
                    // isLoading = true;
                }

            }
        }
    }
}
