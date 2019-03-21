using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Object
{
    public class Snake
    {
        public Vector2[] position;
        public int Length = 2;
        public int WIDTH, HEIGHT, SCALE;
        //0 - вверх, 1 - вправо, 2 - вниз, 3 влево
        public int Direction = 1;
        public int Speed = 1;
        public Snake(int x0, int y0, int Width, int Height)
        {
            WIDTH = Width;
            HEIGHT = Height;
            position = new Vector2[WIDTH * HEIGHT];
            position[0].X = x0;
            position[0].Y = y0;
            for (int i = 1; i < Length; ++i)
            {
                position[i].X = position[i - 1].X - 1;
                position[i].Y = position[0].Y;
            }
        }
        public void Step()
        {
            //Движение змейки
            for (int i = Length; i > 0; --i)
                position[i] = position[i - 1];
            if (Direction == 0)
                position[0].Y -= Speed;
            if (Direction == 1)
                position[0].X += Speed;
            if (Direction == 2)
                position[0].Y += Speed;
            if (Direction == 3)
                position[0].X -= Speed;
            if (position[0].X > (WIDTH))
                position[0].X = 0;
            if (position[0].X < 0)
                position[0].X = WIDTH;
            if (position[0].Y > (HEIGHT))
                position[0].Y = 0;
            if (position[0].Y < 0)
                position[0].Y = HEIGHT;
        }
        public bool CheckSnake(Snake snake)
        {
            //Условие на поедание хвоста
            for (int i = 1; i < Length; ++i)
            {
                if ((position[0].X == snake.position[i].X) && (position[0].Y == snake.position[i].Y))
                    return true;
            }
            return false;
        }
        public Rectangle GetRectangle(int i, int SCALE)
        {
            return (new Rectangle((int)position[i].X * SCALE + Game1.Shift, (int)position[i].Y * SCALE + Game1.Shift, SCALE - 1, SCALE - 1));
        }
        public void CheckApple(Apple apple)
        {
            //Условие на поедание яблока
            if ((position[0].X == apple.position.X) && (position[0].Y == apple.position.Y))
            {
                ++Length;
                ++Game1.Score;
                apple.SetPosition();
            }
        }
    }
}
