using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    static class Controller
    {
        private static bool menuChecker = true;
        public static void Player1()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
                if (Game1.player1.position.Y > 0)
                    Game1.player1.position.Y -= Game1.speedPadle;
            if (keyboardState.IsKeyDown(Keys.S))
                if ((Game1.player1.position.Y + Game1.player1.sprite.Height + 30) < (Game1.height))
                    Game1.player1.position.Y += Game1.speedPadle;
        }
        public static void Player2()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                if (Game1.player2.position.Y > 0)
                    Game1.player2.position.Y -= Game1.speedPadle;
            if (keyboardState.IsKeyDown(Keys.Down))
                if ((Game1.player2.position.Y + Game1.player2.sprite.Height + 30) < (Game1.height))
                    Game1.player2.position.Y += Game1.speedPadle;
        }
        public static void PC()
        {
            if (Game1.player2.position.Y > Game1.ball.position.Y)
                if (Game1.player2.position.Y != 0)
                    Game1.player2.position.Y -= Game1.speedPadle;
            if (Game1.player2.position.Y < Game1.ball.position.Y)
                if ((Game1.player2.position.Y + Game1.player2.sprite.Height) != (Game1.height))
                    Game1.player2.position.Y += Game1.speedPadle;
        }
        static public int Menu()
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.Down)) || (keyState.IsKeyDown(Keys.Up)))
            {
                if (menuChecker)
                {
                    menuChecker = false;
                    Game1.menuStat *= (-1);
                }
            }
            else
            {
                menuChecker = true;
            }
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.menuStat == 1)
                {
                    menuChecker = true;
                    Game1.mode = "Game";
                }
                else
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
