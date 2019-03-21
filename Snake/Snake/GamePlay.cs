using SnakeGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public static class GamePlay
    {
        public static void GameSolo()
        {
            if (Game1.DELAY == 5)
            {
                Controller.GamePlayer1(Game1.snakeOne);
                Game1.snakeOne.CheckApple(Game1.appleOne);
                Game1.appleOne.CheckSnake(Game1.snakeOne);
                Game1.snakeOne.Step();
                if (Game1.snakeOne.CheckSnake(Game1.snakeOne))
                {
                    Game1.GameOverStatus = 1;
                    Game1.Mode = "Game Over";
                }
                Game1.DELAY = 0;
            }
            else
                ++Game1.DELAY;
        }
        public static void GamePVP()
        {
            if (Game1.DELAY == 5)
            {
                Controller.GamePlayer1(Game1.snakeOne);
                Controller.GamePlayer2(Game1.snakeTwo);

                Game1.snakeOne.CheckApple(Game1.appleOne);
                Game1.snakeTwo.CheckApple(Game1.appleOne);
                Game1.snakeOne.CheckApple(Game1.appleTwo);
                Game1.snakeTwo.CheckApple(Game1.appleTwo);

                Game1.appleOne.CheckApple(Game1.appleTwo);
                Game1.appleTwo.CheckApple(Game1.appleOne);

                Game1.appleOne.CheckSnake(Game1.snakeOne);
                Game1.appleTwo.CheckSnake(Game1.snakeOne);
                Game1.appleOne.CheckSnake(Game1.snakeTwo);
                Game1.appleTwo.CheckSnake(Game1.snakeTwo);
                if (Game1.snakeOne.CheckSnake(Game1.snakeOne) || Game1.snakeOne.CheckSnake(Game1.snakeTwo))
                {
                    Game1.GameOverStatus = 3;
                    Game1.Mode = "Game Over";
                }
                Game1.snakeOne.Step();
                if (Game1.snakeTwo.CheckSnake(Game1.snakeOne) || Game1.snakeTwo.CheckSnake(Game1.snakeTwo))
                {
                    Game1.GameOverStatus = 4;
                    Game1.Mode = "Game Over";
                }

                Game1.snakeTwo.Step();
                Game1.DELAY = 0;
            }
            else
                ++Game1.DELAY;
        }
    }
}
