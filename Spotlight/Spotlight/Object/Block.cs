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
        public Texture2D sprite;
        public Vector2 postition;
        public Block(int x, int y, Texture2D sprite)
        {
            postition.X = x;
            postition.Y = y;
            this.sprite = sprite;
        }
        public Rectangle GetRec()
        {
            return new Rectangle((int)postition.X, (int)postition.Y, sprite.Width / 10, sprite.Height / 10);
        }
    }
}
