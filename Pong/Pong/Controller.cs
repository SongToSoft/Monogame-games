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
        private static bool MenuChecker = true;
        public static void Player1()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
                if (Game1.Player1.position.Y > 0)
                    Game1.Player1.position.Y -= Game1.SpeedPadle;
            if (keyboardState.IsKeyDown(Keys.S))
                if ((Game1.Player1.position.Y + Game1.Player1.Sprite.Height + 30) < (Game1.HEIGHT))
                    Game1.Player1.position.Y += Game1.SpeedPadle;
        }
        public static void Player2()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                if (Game1.Player2.position.Y > 0)
                    Game1.Player2.position.Y -= Game1.SpeedPadle;
            if (keyboardState.IsKeyDown(Keys.Down))
                if ((Game1.Player2.position.Y + Game1.Player2.Sprite.Height + 30) < (Game1.HEIGHT))
                    Game1.Player2.position.Y += Game1.SpeedPadle;
        }
        public static void PC()
        {
            if (Game1.Player2.position.Y > Game1.ball.position.Y)
                if (Game1.Player2.position.Y != 0)
                    Game1.Player2.position.Y -= Game1.SpeedPadle;
            if (Game1.Player2.position.Y < Game1.ball.position.Y)
                if ((Game1.Player2.position.Y + Game1.Player2.Sprite.Height) != (Game1.HEIGHT))
                    Game1.Player2.position.Y += Game1.SpeedPadle;
        }
        static public int Menu()
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.Down)) || (keyState.IsKeyDown(Keys.Up)))
            {
                if (MenuChecker)
                {
                    MenuChecker = false;
                    Game1.MenuStat *= (-1);
                }
            }
            else
            {
                MenuChecker = true;
            }
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (Game1.MenuStat == 1)
                {
                    MenuChecker = true;
                    Game1.Mode = "Game";
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
