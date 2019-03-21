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
        public int Status;
        public Texture2D Sprite;
        public Vector2 Position;
        public bool Active;
        public bool Del;
        public Block(int i, int j, int status)
        {
            Active = false;
            Del = false;
            Position.X = j;
            Position.Y = i;
            Status = status;
            Sprite = Operators.SetSprite(Status);
        }
        public Rectangle GetRec()
        {
            Rectangle rec = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);
            return rec;
        }
    }
}
