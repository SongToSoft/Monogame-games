using Microsoft.Xna.Framework.Input;
using Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    //Класс управления
    static class Controller
    {
        private static bool menuChecker = true;
        static public void GamePlayer1(Object.Snake snake)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up) & (snake.Direction != 2))
                snake.Direction = 0;
            if ((keyState.IsKeyDown(Keys.Right)) & (snake.Direction != 3))
                snake.Direction = 1;
            if ((keyState.IsKeyDown(Keys.Down)) & (snake.Direction != 0))
                snake.Direction = 2;
            if ((keyState.IsKeyDown(Keys.Left)) & (snake.Direction != 1))
                snake.Direction = 3;
        }
        static public void GamePlayer2(Object.Snake snake)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W) & (snake.Direction != 2))
                snake.Direction = 0;
            if ((keyState.IsKeyDown(Keys.D)) & (snake.Direction != 3))
                snake.Direction = 1;
            if ((keyState.IsKeyDown(Keys.S)) & (snake.Direction != 0))
                snake.Direction = 2;
            if ((keyState.IsKeyDown(Keys.A)) & (snake.Direction != 1))
                snake.Direction = 3;
        }
        static public int Menu()
        {
            KeyboardState keyState = Keyboard.GetState();

            if ((keyState.IsKeyDown(Keys.Down)))
            {
                if (menuChecker)
                {
                    menuChecker = false;
                    Game1.menuStat += 1;
                    if (Game1.menuStat == 4)
                        Game1.menuStat = 1;
                }
            }
            else
            {
                if ((keyState.IsKeyDown(Keys.Up)))
                {
                    if (menuChecker)
                    {
                        menuChecker = false;
                        Game1.menuStat -= 1;
                        if (Game1.menuStat == 0)
                            Game1.menuStat = 3;
                    }
                }
                else
                    menuChecker = true;
            }
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.menuStat == 1)
                    Game1.mode = "GameSolo";
                if (Game1.menuStat == 2)
                {
                    Game1.mode = "GamePVP";
                    Game1.snakeOne.length = 5;
                    Game1.snakeTwo.length = 5;
                }
                if (Game1.menuStat == 3)
                    return 1;
                menuChecker = true;
            }
            return 0;
        }
        static public void GameOver()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.gameOverStatus == 1)
                    if (Game1.score > Game1.highScore)
                        Reader.SetHighScore(Game1.fullPath);
                Game1.StartGame();
                Game1.mode = "Menu";
            }
        }
    }
}
