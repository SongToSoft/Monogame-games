using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Object
{
    public class Apple
    {
        public Vector2 position;
        private int width, height;
        public Apple(int _width, int _heigth, int ran)
        {
            Random rand = new Random(ran);
            width = _width;
            height = _heigth;
            position.X = (int)(rand.NextDouble() * width);
            position.Y = (int)(rand.NextDouble() * height);
        }
        public void SetPosition()
        {
            Random rand = new Random();
            position.X = (int)(rand.NextDouble() * width);
            position.Y = (int)(rand.NextDouble() * height);
        }
        public Rectangle GetRectangle(int scale)
        {
            return (new Rectangle((int)position.X * scale + Game1.shift, (int)position.Y * scale + Game1.shift, scale - 1, scale - 1));
        }
        public void CheckSnake(Snake snake)
        {
            //Условие на появление яблока внутри змейки
            for (int i = 0; i < snake.length; ++i)
            {
                if ((snake.position[i].X == position.X) && (snake.position[i].Y == position.Y))
                    SetPosition();
            }
        }
        public void CheckApple(Apple apple)
        {
            if ((position.X == apple.position.X) && (position.Y == apple.position.Y))
                SetPosition();
        }
    }
}
