using Match3.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace Match3
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int n = 8;
        public static Block[,] blocks;
        public static Texture2D redSprite, blueSprite, greenSprite, orangeSprite, pinkSprite;
        public static Texture2D ok, play, menuFone, gameFone, gameOverFone;
        public static SpriteFont text;
        private static Song song;
        public static int score = 0, highScore;
        public static float delay = 61;
        public static string mode = "Menu";
        public static string fullPath = Path.GetFullPath("Match3HighScore.txt");
        public static int speed = 1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
            Reader.GetHighScore(fullPath);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ok = Content.Load<Texture2D>("ok");
            play = Content.Load<Texture2D>("PlayButton");
            menuFone = Content.Load<Texture2D>("MenuFone");
            gameFone = Content.Load<Texture2D>("GameFone");
            gameOverFone = Content.Load<Texture2D>("GameOverFone");
            redSprite = Content.Load<Texture2D>("red");
            blueSprite = Content.Load<Texture2D>("blue");
            greenSprite = Content.Load<Texture2D>("green");
            orangeSprite = Content.Load<Texture2D>("orange");
            pinkSprite = Content.Load<Texture2D>("pink");
            song = Content.Load<Song>("Forecast");
            text = Content.Load<SpriteFont>("Text");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            blocks = new Block[n, n];
            Operators.Generate();
            score = 0;
        }

        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (mode == "Menu")
                Controller.Menu();
            if (mode == "Game")
            {
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Controller.Game();
                delay -= timer;
                if (delay <= 0)
                {
                    mode = "Game Over";
                }
            }
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
                spriteBatch.Draw(menuFone, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.DrawString(text, "Click for play", new Vector2(700, 150), Color.Red);
                ///spriteBatch.Draw(Play, new Rectangle(965, 260, Play.Width / 4, Play.Height / 4), Color.White);
            }
            if (mode == "Game")
            {
                spriteBatch.Draw(gameFone, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.DrawString(text, "Score:  " + score, new Vector2(700, 200), Color.DeepSkyBlue);
                spriteBatch.DrawString(text, "Time:  " + (int)delay, new Vector2(700, 400), Color.DeepPink);
                spriteBatch.DrawString(text, "High Score:  " + highScore, new Vector2(700, 600), Color.DarkViolet);
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        if ((blocks[i, j].active == true) || (blocks[i, j].del == true))
                            spriteBatch.Draw(blocks[i, j].sprite, blocks[i, j].GetRec(), Color.Purple);
                        else
                            spriteBatch.Draw(blocks[i, j].sprite, blocks[i, j].GetRec(), Color.White);
                    }
                }
            }
            if (mode == "Game Over")
            {
                spriteBatch.Draw(gameOverFone, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.DrawString(text, "GAME OVER", new Vector2(Window.ClientBounds.Width / 2 - 200, 100), Color.DeepPink);
                spriteBatch.Draw(ok, new Rectangle(Window.ClientBounds.Width / 2 - ok.Width / 2, Window.ClientBounds.Height / 2 - ok.Height / 2 + 200, ok.Width, ok.Height), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
