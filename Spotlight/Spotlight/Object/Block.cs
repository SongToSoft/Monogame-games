using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotlight.Object
{
    public class Block
    {
        public Texture2D Sprite;
        public Vector2 Postition;
        public Block(int X, int Y, Texture2D sprite)
        {
            Postition.X = X;
            Postition.Y = Y;
            Sprite = sprite;
        }
        public Rectangle GetRec()
        {
            return new Rectangle((int)Postition.X, (int)Postition.Y, Sprite.Width / 10, Sprite.Height / 10);
        }
    }
}
