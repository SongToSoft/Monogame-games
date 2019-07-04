using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Object
{
    public class Block
    {
        public int status;
        public Texture2D sprite;
        public Vector2 position;
        public bool active;
        public bool del;
        public Block(int i, int j, int status)
        {
            active = false;
            del = false;
            position.X = j;
            position.Y = i;
            this.status = status;
            sprite = Operators.SetSprite(this.status);
        }
        public Rectangle GetRec()
        {
            Rectangle rec = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            return rec;
        }
    }
}
