using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotlight
{
    public static class Controller
    {
        private static MouseState currentMouseState;
        private static MouseState lastMouseState;
        public static void Menu()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
                Game1.mode = "Game";
        }
        public static void GameOver()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.mode = "Game";
                Game1.level = 1;
                Game1.moves = Game1.level + 4;
                Operators.FillBlocks();
            }
        }
        public static void Game()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            for (int i = 0; i < Game1.n; ++i)
            {
                for (int j = 0; j < Game1.n; ++j)
                {
                    if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
                    {
                        if (Game1.blokcs[i, j].GetRec().Contains(currentMouseState.X, currentMouseState.Y))
                        {
                            Operators.FullSwap(i, j);
                            --Game1.moves;
                        }
                    }
                }
            }
        }
    }
}
