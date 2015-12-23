using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BaseObject;
using BattleField;
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
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        //global variables

        protected Texture2D startButton;
        protected Texture2D exitButton;
        protected Texture2D loadButton;
        protected Texture2D settingsButton;
        protected Texture2D aboutButton;
        protected Texture2D helpButton;
        protected Texture2D backButton;
        protected Texture2D easyDifficultyButton;
        protected Texture2D normalDifficultyButton;
        protected Texture2D hardDifficultyButton;
        protected Texture2D onButton;
        protected Texture2D offButton;
        protected Texture2D lessButton;
        protected Texture2D moreButton;
        protected Texture2D loadFileButton;
        protected Texture2D createRoomButton;
        protected Texture2D joinButton;
        protected Texture2D toBattleButton;
        protected Texture2D localgameButton;
        protected Texture2D networkgameButton;
        protected Texture2D cell_texture;
        protected Texture2D selected_cell_texture;
        protected Texture2D unselected_cell_texture;
        Rectangle cell;
        protected Texture2D loadingScreen;
        protected Texture2D startScreen;
        protected Texture2D title;
        protected Texture2D playScreen;
        
        protected Vector2 startButtonPosition;
        protected Vector2 exitButtonPosition;
        protected Vector2 loadButtonPosition;
        protected Vector2 settingsButtonPosition;
        protected Vector2 aboutButtonPosition;
        protected Vector2 helpButtonPosition;
        protected Vector2 backButtonPosition;
        protected Vector2 easyDifficultyButtonPosition;
        protected Vector2 normalDifficultyButtonPosition;
        protected Vector2 hardDifficultyButtonPosition;
        protected Vector2 onButtonPosition;
        protected Vector2 offButtonPosition;
        protected Vector2 lessButtonPosition;
        protected Vector2 moreButtonPosition;
        protected Vector2 loadFileButtonPosition;
        protected Vector2 toBattleButtonPosition;
        protected Vector2 localgameButtonPosition;
        protected Vector2 networkgameButtonPosition;
        protected Vector2 field2Position;
        protected Vector2 fieldPosition;


        protected Vector2 startScreenPosition;
        protected Vector2 titlePosition;

        protected float volume = 1.0f;
        protected Thread backgroundThread;
        protected bool isLoading = false;
        protected MouseState mouseState;
        protected MouseState previousMouseState;
        GameState gameState;
        GameStarted gameStarted;
        SoundState soundState;
        ShuffleState shuffleState;
        PlacingState placingState;
        protected FileSystem.SaveLoad.DifficultyState difficultyState;
        Song maintheme;
        protected SpriteFont myFont;
        private Rectangle MouseRect;

        Player player;
        Gameboard playerBoard;

        Player enemy;
        Gameboard enemyBoard;


        enum GameState
        {
            StartMenu,
            GameTypeMenu,
            Settings,
            Loading,
            LoadingFinished,
            BattleSettings,
            MPBattleSettings,
            BattlePrep,
            Playing,
            MPBattlePrep,
            MPPlaying,
            MPTypeMenu
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

        enum GameStarted
        {
            Yes,
            No

        }

        private void drawBoard(Gameboard board, Vector2 position)
        {
            if (board.cellList.Count==0)
            for (int coll =0; coll<board.XMax;coll++)
                for (int row = 0; row < board.XMax; row++)
                {
                    if (board.cellList.Count<(board.XMax*board.YMax))
                    {
                        int texture_size=30;
                        switch (board.XMax)
                        {
                                case 10:
                                texture_size = 30;
                                    break;
                                case 15:
                                    texture_size = 20;
                                    break;
                                case 20:
                                    texture_size = 15;
                                    break;
                                default:
                     
                                    break;
                            }
                        cell = new Rectangle(Convert.ToInt32(position.X) + coll* texture_size,
                            Convert.ToInt32(position.Y) + row* texture_size, texture_size,
                            texture_size);
                        board.cellList.Add(cell,Gameboard.State.LIVE);
                            spriteBatch.Draw(cell_texture, cell, Color.White);
                        }
                }
            else
                foreach (var boardCell in board.cellList)
                {
                    if(Gameboard.State.BLOOMER== boardCell.Value)
                    spriteBatch.Draw(cell_texture, boardCell.Key, Color.Black);
                    if (Gameboard.State.DEAD == boardCell.Value)
                        spriteBatch.Draw(cell_texture, boardCell.Key, Color.Red);
                    if (Gameboard.State.LIVE == boardCell.Value)
                        spriteBatch.Draw(cell_texture, boardCell.Key, Color.White);

                }

       



        }

        private void refrashBoard(Gameboard board, float x, float y)
        {
            
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GC.Collect();
           
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
            loadFileButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 200, (GraphicsDevice.Viewport.Height / 2)-50);
            toBattleButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 180);
            startScreenPosition = new Vector2(0, 0);
            titlePosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 200, 150);
            fieldPosition = new Vector2(50, 50);
            field2Position = new Vector2(GraphicsDevice.Viewport.Width - 80, 50);
            //set the gamestate to start menu
            gameState = GameState.StartMenu;
            soundState = SoundState.Off;
            difficultyState = FileSystem.SaveLoad.DifficultyState.Normal;
            shuffleState = ShuffleState.Off;
            placingState = PlacingState.On;
            gameStarted = GameStarted.No;

            //get the mouse state
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;


            base.Initialize();
            cell_texture = unselected_cell_texture;
            FileSystem.SaveLoad.GetDevice();
            GC.Collect();
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
            loadFileButton = Content.Load<Texture2D>("loadfile");
            localgameButton = Content.Load<Texture2D>("localgame");
            networkgameButton = Content.Load<Texture2D>("networkgame");
            createRoomButton = Content.Load<Texture2D>("createroom");
            joinButton = Content.Load<Texture2D>("join");
            loadingScreen = Content.Load<Texture2D>("loading");
            startScreen = Content.Load<Texture2D>("startscreen");
            title = Content.Load<Texture2D>("title");
            unselected_cell_texture = Content.Load<Texture2D>("cell");
            selected_cell_texture = Content.Load<Texture2D>("selectedCell");
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
            

            //move the orb if the game is in progress
            if (gameState == GameState.Playing)
            {
                
            }
            if ((soundState == SoundState.On)&&(MediaPlayer.State==MediaState.Stopped))
            {
                Visualization.Sounds.PlayMusic(maintheme, volume);
            }
            if ((soundState == SoundState.Off) && (MediaPlayer.State==MediaState.Playing))
            {
                Visualization.Sounds.StopMusic();
            }

            //wait for mouseclick
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                
                MouseClicked(mouseState.X, mouseState.Y);
                
            }

            previousMouseState = mouseState;

           
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
            if (gameState == GameState.MPTypeMenu)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(createRoomButton, new Vector2(localgameButtonPosition.X-200,localgameButtonPosition.Y), Color.White);
                spriteBatch.Draw(joinButton, new Vector2(localgameButtonPosition.X + 200, localgameButtonPosition.Y), Color.White);
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
            if (gameState == GameState.MPBattleSettings)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(onButton, onButtonPosition, Color.White);
                spriteBatch.Draw(offButton, offButtonPosition, Color.White);
                spriteBatch.Draw(onButton, new Vector2(onButtonPosition.X, onButtonPosition.Y + 30), Color.White);
                spriteBatch.Draw(offButton, new Vector2(offButtonPosition.X, offButtonPosition.Y + 30), Color.White);
                spriteBatch.Draw(toBattleButton, toBattleButtonPosition, Color.White);
                spriteBatch.Draw(backButton, backButtonPosition, Color.White);
            }
            if (gameState == GameState.Playing)
            {
                    spriteBatch.Draw(playScreen, startScreenPosition, null, Color.White, 0, new Vector2(0, 0), (0.6f), 0,
                        1);
                    //orb
                    if (difficultyState == FileSystem.SaveLoad.DifficultyState.Easy)
                    {

                        spriteBatch.Draw(easyDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30),
                            Color.White);

                    }
                    if (difficultyState == FileSystem.SaveLoad.DifficultyState.Normal)
                    {
                        spriteBatch.Draw(normalDifficultyButton,
                            new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);
                    }
                    if (difficultyState == FileSystem.SaveLoad.DifficultyState.Hard)
                    {
                        spriteBatch.Draw(hardDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30),
                            Color.White);
                    }
                    spriteBatch.Draw(backButton, backButtonPosition, Color.White);


                if (player == null)
                    player = new Player("Геймер", PlayerNumber.One, PlayerType.Human, createShips());
                if (playerBoard == null)
                    playerBoard = new Gameboard(10, 10, player);
                drawBoard(playerBoard, new Vector2(50, 50));
                if (enemy == null)
                    enemy = new Player("Уася", PlayerNumber.Two, PlayerType.Human, createShips());
                if (enemyBoard == null)
                    enemyBoard = new Gameboard(10, 10, enemy);
                drawBoard(enemyBoard, new Vector2(GraphicsDevice.Viewport.Width - (cell.Width * enemyBoard.XMax) - 50, 50));
                spriteBatch.Draw(onButton, aboutButtonPosition, Color.White);
                    spriteBatch.Draw(offButton, helpButtonPosition, Color.White);

                 
                int X = (int)mouseState.X;
                int Y = (int)mouseState.Y;
         
                foreach (var boardCell in enemyBoard.cellList)
                    {
                    //enemyBoard.IsSelectedCell(new Coordinate(X, Y))
     
                        if (boardCell.Key.Intersects(new Rectangle(X, Y, 1, 1)) && boardCell.Value == Gameboard.State.LIVE)
                        {
                        spriteBatch.Draw(selected_cell_texture, boardCell.Key, Color.White);

                        }                
                     }

     





                // spriteBatch.Draw(orb, orbPosition, Color.White);
            }
            if (gameState == GameState.MPPlaying)
            {
                spriteBatch.Draw(playScreen, startScreenPosition, null, Color.White, 0, new Vector2(0, 0), (0.6f), 0, 1);
                //orb
                if (difficultyState == FileSystem.SaveLoad.DifficultyState.Easy)
                {

                    spriteBatch.Draw(easyDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);

                }
                if (difficultyState == FileSystem.SaveLoad.DifficultyState.Normal)
                {
                    spriteBatch.Draw(normalDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);
                }
                if (difficultyState == FileSystem.SaveLoad.DifficultyState.Hard)
                {
                    spriteBatch.Draw(hardDifficultyButton, new Vector2(field2Position.X - 230, fieldPosition.Y - 30), Color.White);
                }



                spriteBatch.Draw(backButton, backButtonPosition, Color.White);

               
                spriteBatch.Draw(onButton, aboutButtonPosition, Color.White);
                spriteBatch.Draw(offButton, helpButtonPosition, Color.White);

                

                // spriteBatch.Draw(orb, orbPosition, Color.White);
            }

            

            if (gameState == GameState.Loading)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(loadFileButton, loadFileButtonPosition, Color.White);
                spriteBatch.Draw(backButton, backButtonPosition, Color.White);


                //spriteBatch.Draw(loadingScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) - (loadingScreen.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (loadingScreen.Height / 2)), Color.YellowGreen);
            }
            if (gameState == GameState.LoadingFinished)
            {
                spriteBatch.Draw(startScreen, startScreenPosition, Color.White);
                spriteBatch.Draw(loadFileButton, loadFileButtonPosition, Color.White);
                spriteBatch.Draw(backButton, backButtonPosition, Color.White);


                spriteBatch.Draw(loadingScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) - (loadingScreen.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (loadingScreen.Height / 2)), Color.YellowGreen);
                Thread.Sleep(500);
            }





            spriteBatch.End();

            base.Draw(gameTime);
        }


        public List<IShip> createShips()
        {
            List<IShip> playerShip = new List<IShip>();
            playerShip.Add(new Ship(1));
            playerShip.Add(new Ship(1));
            playerShip.Add(new Ship(1));
            playerShip.Add(new Ship(1));
            playerShip.Add(new Ship(2));
            playerShip.Add(new Ship(2));
            playerShip.Add(new Ship(2));
            playerShip.Add(new Ship(3));
            playerShip.Add(new Ship(3));
            playerShip.Add(new Ship(4));
            return playerShip;
        }

        void MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            Rectangle mouseClickRect = new Rectangle(x, y, 1, 1);

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
                    gameState = GameState.MPTypeMenu;
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
            if (gameState == GameState.MPTypeMenu)
            {
                
                Rectangle createroomButtonRect = new Rectangle((int)localgameButtonPosition.X-200,
                                            (int)localgameButtonPosition.Y, 400, 100);
                Rectangle joinButtonRect = new Rectangle((int)localgameButtonPosition.X + 200,
                                            (int)localgameButtonPosition.Y, 400, 100);
                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(createroomButtonRect)) //player clicked start button
                {
                    // MP params init

                    gameState = GameState.MPBattleSettings;
                    
                }
                if (mouseClickRect.Intersects(joinButtonRect)) //player clicked start button
                {
                    //Call method to find existing rop
                    gameState = GameState.MPBattlePrep;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    gameState = GameState.GameTypeMenu;
                    // isLoading = true;
                }

            }
            if (gameState == GameState.Loading)
            {

                Rectangle loadFileButtonRect = new Rectangle((int)loadFileButtonPosition.X,
                                            (int)loadFileButtonPosition.Y, 400, 100);
               
                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(loadFileButtonRect)) //player clicked start button
                {

                    //call load save
                    //set backgroundthread
                    backgroundThread = new Thread(FileSystem.SaveLoad.LoadGame);
                    //start backgroundthread
                    backgroundThread.Start();


                    gameState = GameState.LoadingFinished;

                }
                
                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    gameState = GameState.GameTypeMenu;
                    // isLoading = true;
                }

            }
            if (gameState == GameState.LoadingFinished)
            {
                
                gameState = GameState.Playing;
                
                

            }
            if (gameState == GameState.Playing)
            {
                
                Rectangle onButtonRect = new Rectangle((int)aboutButtonPosition.X,
                                           (int)aboutButtonPosition.Y, 100, 20);
                Rectangle offButtonRect = new Rectangle((int)helpButtonPosition.X,
                                            (int)helpButtonPosition.Y, 100, 20);

                Rectangle saveButtonRect = new Rectangle((int)backButtonPosition.X,
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


                if (mouseClickRect.Intersects(saveButtonRect)) //player clicked start button
                {
                    
                    //call save file
                    FileSystem.SaveLoad.SaveGame(difficultyState);
                    gameState = GameState.StartMenu;
                    // isLoading = true;
                }

                if (previousMouseState.LeftButton == ButtonState.Pressed &&
                        mouseState.LeftButton == ButtonState.Released)
                {

                    foreach (var boardCell in enemyBoard.cellList.Keys.ToList())
                    {
                        //enemyBoard.IsSelectedCell(new Coordinate(X, Y))

                        if (boardCell.Intersects(mouseClickRect) && enemyBoard.cellList[boardCell] == Gameboard.State.LIVE)
                        {

                            enemyBoard.cellList[boardCell] = Gameboard.State.BLOOMER;

                        }
                    }

                }


            }
            if (gameState == GameState.MPPlaying)
            {

                Rectangle onButtonRect = new Rectangle((int)aboutButtonPosition.X,
                                           (int)aboutButtonPosition.Y, 100, 20);
                Rectangle offButtonRect = new Rectangle((int)helpButtonPosition.X,
                                            (int)helpButtonPosition.Y, 100, 20);

                Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X,
                                           (int)backButtonPosition.Y, 100, 20);


                 Rectangle fieldRect = new Rectangle((int)fieldPosition.X,
                                           (int)fieldPosition.Y, 300, 300);
                    


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
                if (mouseClickRect.Intersects(fieldRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    int fieldx = (int)(mouseState.X - (int)fieldPosition.X)/30;
                    int fieldy = (int)(mouseState.Y - (int)fieldPosition.Y)/30;
                    
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
            if (gameState == GameState.MPBattleSettings)
            {

                Rectangle shuffleOnButtonRect = new Rectangle((int)onButtonPosition.X,
                                           (int)onButtonPosition.Y, 100, 20);
                Rectangle shuffleOffButtonRect = new Rectangle((int)offButtonPosition.X,
                                            (int)offButtonPosition.Y, 100, 20);
                Rectangle placingOnButtonRect = new Rectangle((int)onButtonPosition.X,
                                           (int)onButtonPosition.Y + 30, 100, 20);
                Rectangle placingOffButtonRect = new Rectangle((int)offButtonPosition.X,
                                            (int)offButtonPosition.Y + 30, 100, 20);
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
                        gameState = GameState.MPPlaying;
                    }
                    else gameState = GameState.MPBattlePrep;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }

                if (mouseClickRect.Intersects(backButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;
                    // gameState = GameState.Playing;
                    gameState = GameState.MPTypeMenu;
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

                    difficultyState = FileSystem.SaveLoad.DifficultyState.Easy;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(normalDifficultyButtonRect)) //player clicked start button
                {
                    difficultyState = FileSystem.SaveLoad.DifficultyState.Normal;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(hardDifficultyButtonRect)) //player clicked start button
                {
                    difficultyState = FileSystem.SaveLoad.DifficultyState.Hard;
                    //gameState = GameState.Playing;
                    // isLoading = true;
                }
                if (mouseClickRect.Intersects(onButtonRect)) //player clicked start button
                {
                    // gameState = GameState.Loading;
                    //gameState = GameState.GameTypeMenu;

                    soundState = SoundState.Off;
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
                   volume=Visualization.Sounds.ChangeVolume(volume,-0.1f);

                    
                }
                if (mouseClickRect.Intersects(moreButtonRect)) //player clicked start button
                {
                    volume=Visualization.Sounds.ChangeVolume(volume,0.1f);
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
