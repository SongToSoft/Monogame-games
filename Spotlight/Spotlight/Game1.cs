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
        public static int level;
        public static int n = 10;
        public static int moves;
        public static string mode = "Menu";

        public static Texture2D light, dark;
        public static Block[,] blokcs;

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
            level = 1;
            moves = level + 3;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            light = Content.Load<Texture2D>("light");
            dark = Content.Load<Texture2D>("dark");
            Text = Content.Load<SpriteFont>("Text");
            blokcs = new Block[n, n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    blokcs[i, j] = new Block(150 + 60 * j, 30 + 60 * i, light);
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
            if (mode == "Menu")
                Controller.Menu();
            if (mode == "Game")
            {
                Controller.Game();
                if (Operators.CheckBlock())
                {
                    for (int i = 0; i < level; ++i)
                    {
                        Operators.Step(i);
                    }
                    moves = level + 4;
                    ++level;
                }
                if (moves == 0)
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
            GraphicsDevice.Clear(new Color(246, 246, 246));
            spriteBatch.Begin();
            if (mode == "Menu")
                spriteBatch.DrawString(Text, "Press Enter", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2), Color.Black);
            if (mode == "Game")
            {
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        spriteBatch.Draw(blokcs[i, j].sprite, blokcs[i, j].GetRec(), Color.White);
                    }
                }
                spriteBatch.DrawString(Text, "Levels = " + (level - 1), new Vector2(800, 100), Color.Black);
                spriteBatch.DrawString(Text, "Moves = " + (moves), new Vector2(800, 200), Color.Black);
            }
            if (mode == "Game Over")
            {
                spriteBatch.DrawString(Text, "Game Over", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 100), Color.Black);
                spriteBatch.DrawString(Text, "Press Enter", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2), Color.Black);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
