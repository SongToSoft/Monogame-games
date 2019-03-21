using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    static class Controller
    {
        private static MouseState currentMouseState;
        private static MouseState lastMouseState;
        private static int MouseStat = 0;
        private static int lastI, lastj;
        public static void Menu()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Rectangle rec = new Rectangle(965, 260, Game1.Play.Width / 4, Game1.Play.Height / 4);
            if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            {
                if (rec.Contains(currentMouseState.X, currentMouseState.Y))
                {
                    Game1.Mode = "Game";
                }
            }
        }
        public static void Shift()
        {
            for (int i = 0; i < Game1.N; ++i)
            {
                for (int j = 0; j < Game1.N; ++j)
                {
                    Game1.Blocks[i, j].Position.X += Game1.Speed;
                }
            }

        }
        public static void Game()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            {
                for (int i = 0; i < Game1.N; ++i)
                {
                    for (int j = 0; j < Game1.N; ++j)
                    {
                        if (Game1.Blocks[i, j].GetRec().Contains(currentMouseState.X, currentMouseState.Y))
                        {
                            if (MouseStat == 0)
                            {
                                Game1.Blocks[i, j].Active = true;
                                MouseStat = 1;
                                lastI = i;
                                lastj = j;
                            }
                            else
                            {
                                MouseStat = 0;
                                if (((i == lastI + 1) && (j == lastj)) ||
                                        ((i == lastI - 1) && (j == lastj)) ||
                                        ((i == lastI) && (j == lastj + 1)) ||
                                        ((i == lastI) && (j == lastj - 1)))
                                {
                                    Operators.SwapBlock(Game1.Blocks[lastI, lastj], Game1.Blocks[i, j]);
                                    if (Operators.Check())
                                    {
                                        while(Operators.Check())
                                            Operators.DelBlocks();
                                    }
                                    else
                                        Operators.SwapBlock(Game1.Blocks[i, j], Game1.Blocks[lastI, lastj]);
                                }
                                Game1.Blocks[lastI, lastj].Active = false;
                            }
                        }
                    }
                }
            }
        }
        public static void GameOver()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            {
                Rectangle rec = new Rectangle(Game1.graphics.PreferredBackBufferWidth / 2 - Game1.Ok.Width / 2, Game1.graphics.PreferredBackBufferHeight / 2 - Game1.Ok.Height / 2 + 200, Game1.Ok.Width, Game1.Ok.Height);
                if (rec.Contains(currentMouseState.X, currentMouseState.Y))
                {
                    if (Game1.Score > Game1.HighScore)
                    {
                        Reader.SetHighScore(Game1.fullPath);
                        Reader.GetHighScore(Game1.fullPath);
                    }
                    Game1.Score = 0;
                    Game1.Delay = 61;
                    Game1.Mode = "Menu";
                }
            }
        }
    }
}
