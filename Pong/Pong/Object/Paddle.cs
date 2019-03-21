using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Pong.Object
{
    public class Paddle
    {
        public Vector2 position;
        public Texture2D Sprite;
        private int SizeWidth = 5;
        public Rectangle GetRec()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, SizeWidth, Sprite.Height + 30);
            return Rec;
        }
        public Rectangle GetRecUp()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, SizeWidth, 2);
            return Rec;
        }
        public Rectangle GetRecDown()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y + Sprite.Height + 30, SizeWidth, 2);
            return Rec;
        }
        public Paddle(int X, int Y, Texture2D sprite)
        {
            position.X = X;
            position.Y = Y;
            Sprite = sprite;
        }
    }
}
