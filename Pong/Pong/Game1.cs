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
        private Texture2D spritePaddle, spriteBall, spriteBorder;
        public static Paddle player1, player2;
        public static Ball ball;
        private SpriteFont point;
        public static float speedPadle, speedBall;
        public static int width, height;
        public static int player1Point, player2Point;
        public static string mode = "Menu";
        public static int menuStat = 1;
        public static Song bhp, bhw, pmb;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            Content.RootDirectory = "Content";
            player1Point = 0;
            player2Point = 0;
            speedPadle = 6;
            speedBall = 5;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritePaddle = Content.Load<Texture2D>("Paddle");
            spriteBall = Content.Load<Texture2D>("Ball");
            spriteBorder = Content.Load<Texture2D>("Border");
            point = Content.Load<SpriteFont>("Point");
            bhp = Content.Load<Song>("BallHitPaddle");
            bhw = Content.Load<Song>("BallHitWall");
            pmb = Content.Load<Song>("PlayerMissBall");
            player1 = new Paddle(50, 200, spritePaddle);
            player2 = new Paddle(width - 50, height - 250, spritePaddle);
            ball = new Ball(width / 2 - spriteBall.Width, height / 2 - spriteBall.Height, 1, spriteBall);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (mode == "Menu")
            {
                if (Controller.Menu() == 1)
                {
                    Exit();
                }
            }
            if (mode == "Game")
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
            if (mode == "Menu")
            {
                spriteBatch.DrawString(point, "Start", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                spriteBatch.DrawString(point, "Exit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 75, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
                if (menuStat == 1)
                    spriteBatch.DrawString(point, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 25), Color.White);
                else
                    spriteBatch.DrawString(point, ">", new Vector2(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 + 25), Color.White);
            }
            if (mode == "Game")
            {
                spriteBatch.DrawString(point, " " + player1Point, new Vector2((Window.ClientBounds.Width / 2 - 75), 50), Color.White);
                spriteBatch.DrawString(point, " " + player2Point, new Vector2((Window.ClientBounds.Width / 2 + 25), 50), Color.White);
                spriteBatch.Draw(spriteBorder, new Rectangle(Window.ClientBounds.Width / 2 - spriteBorder.Width, 0, spriteBorder.Width, height), Color.White);
                spriteBatch.Draw(player1.sprite, player1.GetRec(), Color.Red);
                spriteBatch.Draw(player2.sprite, player2.GetRec(), Color.Blue);
                spriteBatch.Draw(ball.sprite, ball.GetRec(), Color.Yellow);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
