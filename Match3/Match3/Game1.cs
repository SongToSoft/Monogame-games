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
        public static int N = 8;
        public static Block[,] Blocks;
        public static Texture2D RedSprite, BlueSprite, GreenSprite, OrangeSprite, PinkSprite;
        public static Texture2D Ok, Play, MenuFone, GameFone, GameOverFone;
        public static SpriteFont Text;
        private static Song song;
        public static int Score = 0, HighScore;
        public static float Delay = 61;
        public static string Mode = "Menu";
        public static string fullPath = Path.GetFullPath("Match3HighScore.txt");
        public static int Speed = 1;
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
            Ok = Content.Load<Texture2D>("ok");
            Play = Content.Load<Texture2D>("PlayButton");
            MenuFone = Content.Load<Texture2D>("MenuFone");
            GameFone = Content.Load<Texture2D>("GameFone");
            GameOverFone = Content.Load<Texture2D>("GameOverFone");
            RedSprite = Content.Load<Texture2D>("red");
            BlueSprite = Content.Load<Texture2D>("blue");
            GreenSprite = Content.Load<Texture2D>("green");
            OrangeSprite = Content.Load<Texture2D>("orange");
            PinkSprite = Content.Load<Texture2D>("pink");
            song = Content.Load<Song>("Forecast");
            Text = Content.Load<SpriteFont>("Text");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            Blocks = new Block[N, N];
            Operators.Generate();
            Score = 0;
        }

        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mode == "Menu")
                Controller.Menu();
            if (Mode == "Game")
            {
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Controller.Game();
                Delay -= timer;
                if (Delay <= 0)
                {
                    Mode = "Game Over";
                }
            }
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
                spriteBatch.Draw(MenuFone, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(Play, new Rectangle(965, 260, Play.Width / 4, Play.Height / 4), Color.White);
            }
            if (Mode == "Game")
            {
                spriteBatch.Draw(GameFone, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.DrawString(Text, "Score:  " + Score, new Vector2(700, 200), Color.DeepSkyBlue);
                spriteBatch.DrawString(Text, "Time:  " + (int)Delay, new Vector2(700, 400), Color.DeepPink);
                spriteBatch.DrawString(Text, "High Score:  " + HighScore, new Vector2(700, 600), Color.DarkViolet);
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        if ((Blocks[i, j].Active == true) || (Blocks[i, j].Del == true))
                            spriteBatch.Draw(Blocks[i, j].Sprite, Blocks[i, j].GetRec(), Color.Purple);
                        else
                            spriteBatch.Draw(Blocks[i, j].Sprite, Blocks[i, j].GetRec(), Color.White);
                    }
                }
            }
            if (Mode == "Game Over")
            {
                spriteBatch.Draw(GameOverFone, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.DrawString(Text, "GAME OVER", new Vector2(Window.ClientBounds.Width / 2 - 200, 100), Color.DeepPink);
                spriteBatch.Draw(Ok, new Rectangle(Window.ClientBounds.Width / 2 - Ok.Width / 2, Window.ClientBounds.Height / 2 - Ok.Height / 2 + 200, Ok.Width, Ok.Height), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
