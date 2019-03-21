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
                Game1.Mode = "Game";
        }
        public static void GameOver()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.Mode = "Game";
                Game1.Level = 1;
                Game1.Moves = Game1.Level + 4;
                Operators.FillBlocks();
            }
        }
        public static void Game()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            for (int i = 0; i < Game1.N; ++i)
            {
                for (int j = 0; j < Game1.N; ++j)
                {
                    if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
                    {
                        if (Game1.Blokcs[i, j].GetRec().Contains(currentMouseState.X, currentMouseState.Y))
                        {
                            Operators.FullSwap(i, j);
                            --Game1.Moves;
                        }
                    }
                }
            }
        }
    }
}
