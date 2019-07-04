using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Pong.Object
{
    public class Ball
    {
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 direction;

        public Rectangle GetRec()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            return Rec;
        }
        public Ball(int x, int y, int dir, Texture2D sprite)
        {
            position.X = x;
            position.Y = y;
            direction.X = dir;
            direction.Y = direction.X;
            this.sprite = sprite;
        }
        public void Move()
        {
            //Движение мяча
            position.X += (direction.X) * 2 * Game1.speedBall;
            position.Y += (direction.Y) * 2 * Game1.speedBall;
            if (position.X <= 0)
            {
                direction.X *= (-1);
                position.X = Game1.width / 2 - sprite.Width;
                position.Y = Game1.height / 2 - sprite.Height;
                direction.Y = direction.X;
                ++Game1.player2Point;
                MediaPlayer.Play(Game1.pmb);
            }
            if ((position.X + sprite.Width) >= (Game1.width))
            {
                direction.X *= (-1);
                position.X = Game1.width / 2 - sprite.Width;
                position.Y = Game1.height / 2 - sprite.Height;
                direction.Y = direction.X;
                ++Game1.player1Point;
                MediaPlayer.Play(Game1.pmb);
            }
            if (position.Y <= 0)
            {
                direction.Y *= (-1);
                MediaPlayer.Play(Game1.bhw);
            }
            if ((position.Y + sprite.Height) >= (Game1.height))
            {
                direction.Y *= (-1);
                MediaPlayer.Play(Game1.bhw);
            }
            if ((GetRec().Intersects(Game1.player1.GetRec())) || (GetRec().Intersects(Game1.player2.GetRec())))
            {
                direction.X *= (-1);
                //if ((position.Y == Game1.Player1.position.Y) || (position.Y == Game1.Player2.position.Y))
                //    DirectionY *= (-1);
                if (GetRec().Intersects(Game1.player1.GetRecUp()) || GetRec().Intersects(Game1.player1.GetRecDown()))
                {
                    direction.Y *= (-1);
                }
                if (GetRec().Intersects(Game1.player2.GetRecUp()) || GetRec().Intersects(Game1.player2.GetRecDown()))
                {
                    direction.Y *= (-1);
                }
                MediaPlayer.Play(Game1.bhp);
            }
        }
    }
}
