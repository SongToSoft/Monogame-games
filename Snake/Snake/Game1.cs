using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Snake;
using System;
using System.IO;

namespace SnakeGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D BlockTexture, AppleTexture;
        private Texture2D HorizonBoard, VerticalBoard;
        private SpriteFont Text;
        private Song song;
        private static int SCALE = 16;
        public static int DELAY;
        public static int WIDTH = 40, HEIGHT = 40;
        public static int HighScore;
        public static Object.Apple appleOne, appleTwo;
        public static Object.Snake snakeOne, snakeTwo;
        public static int Score = 0;
        public static int Shift = 50;
        public static string Mode = "Menu";
        public static int MenuStat = 1;
        public static string fullPath;
        public static int GameOverStatus = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SCALE * WIDTH;
            graphics.PreferredBackBufferHeight = SCALE * HEIGHT;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            fullPath = Path.GetFullPath("SnakeGameHighScore.txt");
        }
        public static void StartGame()
        {
            Reader.GetHighScore(fullPath); 
            Score = 0;
            snakeOne = new Object.Snake(25, 15, WIDTH, HEIGHT);
            snakeTwo = new Object.Snake(15, 5, WIDTH, HEIGHT);
            appleOne = new Object.Apple(WIDTH, HEIGHT, 1);
            appleTwo = new Object.Apple(WIDTH, HEIGHT, 10);
            GameOverStatus = 0;
        }
        protected override void Initialize()
        {
            StartGame();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            DELAY = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BlockTexture = Content.Load<Texture2D>("SnakeBlock");
            AppleTexture = Content.Load<Texture2D>("Apple");
            HorizonBoard = Content.Load<Texture2D>("HorizonBoard");
            VerticalBoard = Content.Load<Texture2D>("VerticalBoard");
            Text = Content.Load<SpriteFont>("Text");
            song = Content.Load<Song>("Song");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mode == "Menu")
                if (Controller.Menu() == 1)
                    Exit();
            if (Mode == "GameSolo")
                GamePlay.GameSolo();
            if (Mode == "GamePVP")
                GamePlay.GamePVP();
            if (Mode == "Game Over")
                Controller.GameOver();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (Mode == "Menu")
            {
                spriteBatch.DrawString(Text, "Start Solo", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                spriteBatch.DrawString(Text, "Start PvP", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                spriteBatch.DrawString(Text, "Exit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 75), Color.White);
                if (MenuStat == 1)
                    spriteBatch.DrawString(Text, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                if (MenuStat == 2)
                    spriteBatch.DrawString(Text, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                if (MenuStat == 3)
                    spriteBatch.DrawString(Text, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 + 75), Color.White);
            }
            if (Mode == "GameSolo")
            {
                spriteBatch.Draw(HorizonBoard, new Rectangle(Shift, Shift, 1, SCALE * HEIGHT + SCALE), Color.White);
                spriteBatch.Draw(VerticalBoard, new Rectangle(Shift, Shift, SCALE * WIDTH + SCALE, 1), Color.White);
                spriteBatch.Draw(HorizonBoard, new Rectangle(Shift + SCALE * HEIGHT + SCALE, Shift, 1, SCALE * HEIGHT + SCALE), Color.White);
                spriteBatch.Draw(VerticalBoard, new Rectangle(Shift, Shift + SCALE * WIDTH + SCALE, SCALE * WIDTH + SCALE, 1), Color.White);
                for (int i = 0; i < snakeOne.Length; ++i)
                {
                    spriteBatch.Draw(BlockTexture, snakeOne.GetRectangle(i, SCALE), Color.White);
                }
                spriteBatch.Draw(AppleTexture, appleOne.GetRectangle(SCALE), Color.White);
                spriteBatch.DrawString(Text, "Score:  " + Score, new Vector2(SCALE * WIDTH + SCALE + 100, Shift), Color.White);
                spriteBatch.DrawString(Text, "High Score:  " + HighScore, new Vector2(SCALE * WIDTH + SCALE + 100, Shift * 2), Color.White);
            }
            if (Mode == "GamePVP")
            {
                spriteBatch.Draw(HorizonBoard, new Rectangle(Shift, Shift, 1, SCALE * HEIGHT + SCALE), Color.White);
                spriteBatch.Draw(VerticalBoard, new Rectangle(Shift, Shift, SCALE * WIDTH + SCALE, 1), Color.White);
                spriteBatch.Draw(HorizonBoard, new Rectangle(Shift + SCALE * HEIGHT + SCALE, Shift, 1, SCALE * HEIGHT + SCALE), Color.White);
                spriteBatch.Draw(VerticalBoard, new Rectangle(Shift, Shift + SCALE * WIDTH + SCALE, SCALE * WIDTH + SCALE, 1), Color.White);
                for (int i = 0; i < snakeOne.Length; ++i)
                {
                    spriteBatch.Draw(BlockTexture, snakeOne.GetRectangle(i, SCALE), Color.Yellow);
                }
                for (int i = 0; i < snakeTwo.Length; ++i)
                {
                    spriteBatch.Draw(BlockTexture, snakeTwo.GetRectangle(i, SCALE), Color.Blue);
                }
                spriteBatch.Draw(AppleTexture, appleOne.GetRectangle(SCALE), Color.White);
                spriteBatch.Draw(AppleTexture, appleTwo.GetRectangle(SCALE), Color.White);
                spriteBatch.DrawString(Text, "Score Player 1:  " + (snakeOne.Length - 5), new Vector2(SCALE * WIDTH + SCALE + 100, Shift), Color.White);
                spriteBatch.DrawString(Text, "Score Player 2:  " + (snakeTwo.Length - 5), new Vector2(SCALE * WIDTH + SCALE + 100, Shift * 2), Color.White);
            }
            if (Mode == "Game Over")
            {
                if (GameOverStatus == 1)
                {
                    spriteBatch.DrawString(Text, "Game Over", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                    spriteBatch.DrawString(Text, "Your Score: " + Score, new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                }
                if (GameOverStatus == 3)
                    spriteBatch.DrawString(Text, "Player One Lose - Player Two Win", new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                if (GameOverStatus == 4)
                    spriteBatch.DrawString(Text, "Player Two Lose - Player One Win", new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                spriteBatch.DrawString(Text, "Retry?", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 75), Color.White);
                
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}