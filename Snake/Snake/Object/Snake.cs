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
        public int length = 2;
        public int width, heigth, scale;
        //0 - вверх, 1 - вправо, 2 - вниз, 3 влево
        public int Direction = 1;
        public int Speed = 1;
        public Snake(int x0, int y0, int _width, int _height)
        {
            width = _width;
            heigth = _height;
            position = new Vector2[width * heigth];
            position[0].X = x0;
            position[0].Y = y0;
            for (int i = 1; i < length; ++i)
            {
                position[i].X = position[i - 1].X - 1;
                position[i].Y = position[0].Y;
            }
        }
        public void Step()
        {
            //Движение змейки
            for (int i = length; i > 0; --i)
                position[i] = position[i - 1];
            if (Direction == 0)
                position[0].Y -= Speed;
            if (Direction == 1)
                position[0].X += Speed;
            if (Direction == 2)
                position[0].Y += Speed;
            if (Direction == 3)
                position[0].X -= Speed;
            if (position[0].X > (width))
                position[0].X = 0;
            if (position[0].X < 0)
                position[0].X = width;
            if (position[0].Y > (heigth))
                position[0].Y = 0;
            if (position[0].Y < 0)
                position[0].Y = heigth;
        }
        public bool CheckSnake(Snake snake)
        {
            //Условие на поедание хвоста
            for (int i = 1; i < snake.length; ++i)
            {
                if ((position[0].X == snake.position[i].X) && (position[0].Y == snake.position[i].Y))
                    return true;
            }
            return false;
        }
        public Rectangle GetRectangle(int i, int _scale)
        {
            return (new Rectangle((int)position[i].X * _scale + Game1.shift, (int)position[i].Y * _scale + Game1.shift, _scale - 1, _scale - 1));
        }
        public void CheckApple(Apple apple)
        {
            //Условие на поедание яблока
            if ((position[0].X == apple.position.X) && (position[0].Y == apple.position.Y))
            {
                ++length;
                ++Game1.score;
                apple.SetPosition();
            }
        }
    }
}
