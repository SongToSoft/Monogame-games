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
        private int WIDTH, HEIGHT;
        public Apple(int Width, int Heigth, int ran)
        {
            Random rand = new Random(ran);
            WIDTH = Width;
            HEIGHT = Heigth;
            position.X = (int)(rand.NextDouble() * WIDTH);
            position.Y = (int)(rand.NextDouble() * HEIGHT);
        }
        public void SetPosition()
        {
            Random rand = new Random();
            position.X = (int)(rand.NextDouble() * WIDTH);
            position.Y = (int)(rand.NextDouble() * HEIGHT);
        }
        public Rectangle GetRectangle(int SCALE)
        {
            return (new Rectangle((int)position.X * SCALE + Game1.Shift, (int)position.Y * SCALE + Game1.Shift, SCALE - 1, SCALE - 1));
        }
        public void CheckSnake(Snake snake)
        {
            //Условие на появление яблока внутри змейки
            for (int i = 0; i < snake.Length; ++i)
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
