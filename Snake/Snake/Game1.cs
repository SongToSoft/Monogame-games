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
        private Texture2D blockTexture, appleTexture;
        private Texture2D horizonBoard, verticalBoard;
        private SpriteFont text;
        private Song song;
        private static int scale = 16;
        public static int delay;
        public static int width = 40, heigth = 40;
        public static int highScore;
        public static Object.Apple appleOne, appleTwo;
        public static Object.Snake snakeOne, snakeTwo;
        public static int score = 0;
        public static int shift = 50;
        public static string mode = "Menu";
        public static int menuStat = 1;
        public static string fullPath;
        public static int gameOverStatus = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = scale * width;
            graphics.PreferredBackBufferHeight = scale * heigth;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            fullPath = Path.GetFullPath("SnakeGameHighScore.txt");
        }
        public static void StartGame()
        {
            Reader.GetHighScore(fullPath); 
            score = 0;
            snakeOne = new Object.Snake(25, 15, width, heigth);
            snakeTwo = new Object.Snake(15, 5, width, heigth);
            appleOne = new Object.Apple(width, heigth, 1);
            appleTwo = new Object.Apple(width, heigth, 10);
            gameOverStatus = 0;
        }
        protected override void Initialize()
        {
            StartGame();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            delay = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blockTexture = Content.Load<Texture2D>("SnakeBlock");
            appleTexture = Content.Load<Texture2D>("Apple");
            horizonBoard = Content.Load<Texture2D>("HorizonBoard");
            verticalBoard = Content.Load<Texture2D>("VerticalBoard");
            text = Content.Load<SpriteFont>("Text");
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
            if (mode == "Menu")
                if (Controller.Menu() == 1)
                    Exit();
            if (mode == "GameSolo")
                GamePlay.GameSolo();
            if (mode == "GamePVP")
                GamePlay.GamePVP();
            if (mode == "Game Over")
                Controller.GameOver();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (mode == "Menu")
            {
                spriteBatch.DrawString(text, "Start Solo", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                spriteBatch.DrawString(text, "Start PvP", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                spriteBatch.DrawString(text, "Exit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 75), Color.White);
                if (menuStat == 1)
                    spriteBatch.DrawString(text, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                if (menuStat == 2)
                    spriteBatch.DrawString(text, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                if (menuStat == 3)
                    spriteBatch.DrawString(text, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 + 75), Color.White);
            }
            if (mode == "GameSolo")
            {
                spriteBatch.Draw(horizonBoard, new Rectangle(shift, shift, 1, scale * heigth + scale), Color.White);
                spriteBatch.Draw(verticalBoard, new Rectangle(shift, shift, scale * width + scale, 1), Color.White);
                spriteBatch.Draw(horizonBoard, new Rectangle(shift + scale * heigth + scale, shift, 1, scale * heigth + scale), Color.White);
                spriteBatch.Draw(verticalBoard, new Rectangle(shift, shift + scale * width + scale, scale * width + scale, 1), Color.White);
                for (int i = 0; i < snakeOne.length; ++i)
                {
                    spriteBatch.Draw(blockTexture, snakeOne.GetRectangle(i, scale), Color.White);
                }
                spriteBatch.Draw(appleTexture, appleOne.GetRectangle(scale), Color.White);
                spriteBatch.DrawString(text, "Score:  " + score, new Vector2(scale * width + scale + 100, shift), Color.White);
                spriteBatch.DrawString(text, "High Score:  " + highScore, new Vector2(scale * width + scale + 100, shift * 2), Color.White);
            }
            if (mode == "GamePVP")
            {
                spriteBatch.Draw(horizonBoard, new Rectangle(shift, shift, 1, scale * heigth + scale), Color.White);
                spriteBatch.Draw(verticalBoard, new Rectangle(shift, shift, scale * width + scale, 1), Color.White);
                spriteBatch.Draw(horizonBoard, new Rectangle(shift + scale * heigth + scale, shift, 1, scale * heigth + scale), Color.White);
                spriteBatch.Draw(verticalBoard, new Rectangle(shift, shift + scale * width + scale, scale * width + scale, 1), Color.White);
                for (int i = 0; i < snakeOne.length; ++i)
                {
                    spriteBatch.Draw(blockTexture, snakeOne.GetRectangle(i, scale), Color.Yellow);
                }
                for (int i = 0; i < snakeTwo.length; ++i)
                {
                    spriteBatch.Draw(blockTexture, snakeTwo.GetRectangle(i, scale), Color.Blue);
                }
                spriteBatch.Draw(appleTexture, appleOne.GetRectangle(scale), Color.White);
                spriteBatch.Draw(appleTexture, appleTwo.GetRectangle(scale), Color.White);
                spriteBatch.DrawString(text, "Score Player 1:  " + (snakeOne.length - 5), new Vector2(scale * width + scale + 100, shift), Color.White);
                spriteBatch.DrawString(text, "Score Player 2:  " + (snakeTwo.length - 5), new Vector2(scale * width + scale + 100, shift * 2), Color.White);
            }
            if (mode == "Game Over")
            {
                if (gameOverStatus == 1)
                {
                    spriteBatch.DrawString(text, "Game Over", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                    spriteBatch.DrawString(text, "Your Score: " + score, new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                }
                if (gameOverStatus == 3)
                    spriteBatch.DrawString(text, "Player One Lose - Player Two Win", new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                if (gameOverStatus == 4)
                    spriteBatch.DrawString(text, "Player Two Lose - Player One Win", new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                spriteBatch.DrawString(text, "Retry?", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 75), Color.White);
                
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}