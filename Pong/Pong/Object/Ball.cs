using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Pong.Object
{
    public class Ball
    {
        public Texture2D Sprite;
        public Vector2 position;
        public Vector2 Direction;

        public Rectangle GetRec()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, Sprite.Width, Sprite.Height);
            return Rec;
        }
        public Ball(int X, int Y, int Dir, Texture2D sprite)
        {
            position.X = X;
            position.Y = Y;
            Direction.X = Dir;
            Direction.Y = Direction.X;
            Sprite = sprite;
        }
        public void Move()
        {
            //Движение мяча
            position.X += (Direction.X) * 2 * Game1.SpeedBall;
            position.Y += (Direction.Y) * 2 * Game1.SpeedBall;
            if (position.X <= 0)
            {
                Direction.X *= (-1);
                position.X = Game1.WIDTH / 2 - Sprite.Width;
                position.Y = Game1.HEIGHT / 2 - Sprite.Height;
                Direction.Y = Direction.X;
                ++Game1.Player2Point;
                MediaPlayer.Play(Game1.PMB);
            }
            if ((position.X + Sprite.Width) >= (Game1.WIDTH))
            {
                Direction.X *= (-1);
                position.X = Game1.WIDTH / 2 - Sprite.Width;
                position.Y = Game1.HEIGHT / 2 - Sprite.Height;
                Direction.Y = Direction.X;
                ++Game1.Player1Point;
                MediaPlayer.Play(Game1.PMB);
            }
            if (position.Y <= 0)
            {
                Direction.Y *= (-1);
                MediaPlayer.Play(Game1.BHW);
            }
            if ((position.Y + Sprite.Height) >= (Game1.HEIGHT))
            {
                Direction.Y *= (-1);
                MediaPlayer.Play(Game1.BHW);
            }
            if ((GetRec().Intersects(Game1.Player1.GetRec())) || (GetRec().Intersects(Game1.Player2.GetRec())))
            {
                Direction.X *= (-1);
                //if ((position.Y == Game1.Player1.position.Y) || (position.Y == Game1.Player2.position.Y))
                //    DirectionY *= (-1);
                if (GetRec().Intersects(Game1.Player1.GetRecUp()) || GetRec().Intersects(Game1.Player1.GetRecDown()))
                {
                    Direction.Y *= (-1);
                }
                if (GetRec().Intersects(Game1.Player2.GetRecUp()) || GetRec().Intersects(Game1.Player2.GetRecDown()))
                {
                    Direction.Y *= (-1);
                }
                MediaPlayer.Play(Game1.BHP);
            }
        }
    }
}
