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
        public Texture2D sprite;
        private int sizeWidth = 5;
        public Rectangle GetRec()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, sizeWidth, sprite.Height + 30);
            return Rec;
        }
        public Rectangle GetRecUp()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y, sizeWidth, 2);
            return Rec;
        }
        public Rectangle GetRecDown()
        {
            Rectangle Rec = new Rectangle((int)position.X, (int)position.Y + sprite.Height + 30, sizeWidth, 2);
            return Rec;
        }
        public Paddle(int x, int y, Texture2D sprite)
        {
            position.X = x;
            position.Y = y;
            this.sprite = sprite;
        }
    }
}
