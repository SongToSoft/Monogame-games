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
        private static bool MenuChecker = true;
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
                if (MenuChecker)
                {
                    MenuChecker = false;
                    Game1.MenuStat += 1;
                    if (Game1.MenuStat == 4)
                        Game1.MenuStat = 1;
                }
            }
            else
            {
                if ((keyState.IsKeyDown(Keys.Up)))
                {
                    if (MenuChecker)
                    {
                        MenuChecker = false;
                        Game1.MenuStat -= 1;
                        if (Game1.MenuStat == 0)
                            Game1.MenuStat = 3;
                    }
                }
                else
                    MenuChecker = true;
            }
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.MenuStat == 1)
                    Game1.Mode = "GameSolo";
                if (Game1.MenuStat == 2)
                {
                    Game1.Mode = "GamePVP";
                    Game1.snakeOne.Length = 5;
                    Game1.snakeTwo.Length = 5;
                }
                if (Game1.MenuStat == 3)
                    return 1;
                MenuChecker = true;
            }
            return 0;
        }
        static public void GameOver()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.GameOverStatus == 1)
                    if (Game1.Score > Game1.HighScore)
                        Reader.SetHighScore(Game1.fullPath);
                Game1.StartGame();
                Game1.Mode = "Menu";
            }
        }
    }
}
