using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Object;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D SpritePaddle, SpriteBall, SpriteBorder;
        public static Paddle Player1, Player2;
        public static Ball ball;
        private SpriteFont Point;
        public static float SpeedPadle, SpeedBall;
        public static int WIDTH, HEIGHT;
        public static int Player1Point, Player2Point;
        public static string Mode = "Menu";
        public static int MenuStat = 1;
        public static Song BHP, BHW, PMB;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            WIDTH = graphics.PreferredBackBufferWidth;
            HEIGHT = graphics.PreferredBackBufferHeight;
            Content.RootDirectory = "Content";
            Player1Point = 0;
            Player2Point = 0;
            SpeedPadle = 6;
            SpeedBall = 5;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpritePaddle = Content.Load<Texture2D>("Paddle");
            SpriteBall = Content.Load<Texture2D>("Ball");
            SpriteBorder = Content.Load<Texture2D>("Border");
            Point = Content.Load<SpriteFont>("Point");
            BHP = Content.Load<Song>("BallHitPaddle");
            BHW = Content.Load<Song>("BallHitWall");
            PMB = Content.Load<Song>("PlayerMissBall");
            Player1 = new Paddle(50, 200, SpritePaddle);
            Player2 = new Paddle(WIDTH - 50, HEIGHT - 250, SpritePaddle);
            ball = new Ball(WIDTH / 2 - SpriteBall.Width, HEIGHT / 2 - SpriteBall.Height, 1, SpriteBall);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mode == "Menu")
            {
                if (Controller.Menu() == 1)
                {
                    Exit();
                }
            }
            if (Mode == "Game")
            {
                Controller.Player1();
                Controller.Player2();
                ball.Move();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (Mode == "Menu")
            {
                spriteBatch.DrawString(Point, "Start", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                spriteBatch.DrawString(Point, "Exit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                if (MenuStat == 1)
                    spriteBatch.DrawString(Point, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                else
                    spriteBatch.DrawString(Point, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
            }
            if (Mode == "Game")
            {
                spriteBatch.DrawString(Point, " " + Player1Point, new Vector2((Window.ClientBounds.Width / 2 - 75), 50), Color.White);
                spriteBatch.DrawString(Point, " " + Player2Point, new Vector2((Window.ClientBounds.Width / 2 - SpriteBorder.Width), 50), Color.White);
                spriteBatch.Draw(SpriteBorder, new Rectangle(Window.ClientBounds.Width / 2 - SpriteBorder.Width, 0, SpriteBorder.Width, HEIGHT), Color.White);
                spriteBatch.Draw(Player1.Sprite, Player1.GetRec(), Color.Red);
                spriteBatch.Draw(Player2.Sprite, Player2.GetRec(), Color.Blue);
                spriteBatch.Draw(ball.Sprite, ball.GetRec(), Color.Yellow);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
