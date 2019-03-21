using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spotlight.Object;

namespace Spotlight
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int Level;
        public static int N = 10;
        public static int Moves;
        public static string Mode = "Menu";

        public static Texture2D light, dark;
        public static Block[,] Blokcs;

        private SpriteFont Text;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Level = 1;
            Moves = Level + 3;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            light = Content.Load<Texture2D>("light");
            dark = Content.Load<Texture2D>("dark");
            Text = Content.Load<SpriteFont>("Text");
            Blokcs = new Block[N, N];
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    Blokcs[i, j] = new Block(150 + 60 * j, 30 + 60 * i, light);
                }
            }
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
                Controller.Game();
                if (Operators.CheckBlock())
                {
                    for (int i = 0; i < Level; ++i)
                    {
                        Operators.Step(i);
                    }
                    Moves = Level + 4;
                    ++Level;
                }
                if (Moves == 0)
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
            GraphicsDevice.Clear(new Color(246, 246, 246));
            spriteBatch.Begin();
            if (Mode == "Menu")
                spriteBatch.DrawString(Text, "Press Enter", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2), Color.Black);
            if (Mode == "Game")
            {
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        spriteBatch.Draw(Blokcs[i, j].Sprite, Blokcs[i, j].GetRec(), Color.White);
                    }
                }
                spriteBatch.DrawString(Text, "Levels = " + (Level - 1), new Vector2(800, 100), Color.Black);
                spriteBatch.DrawString(Text, "Moves = " + (Moves), new Vector2(800, 200), Color.Black);
            }
            if (Mode == "Game Over")
            {
                spriteBatch.DrawString(Text, "Game Over", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 100), Color.Black);
                spriteBatch.DrawString(Text, "Press Enter", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2), Color.Black);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
