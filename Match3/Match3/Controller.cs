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
        private static int mouseStat = 0;
        private static int lastI, lastj;
        public static void Menu()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Rectangle rec = new Rectangle(965, 260, Game1.play.Width / 4, Game1.play.Height / 4);
            //if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            //{
            //    if (rec.Contains(currentMouseState.X, currentMouseState.Y))
            //    {
            //        Game1.Mode = "Game";
            //    }
            //}
            if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            {
                Game1.mode = "Game";
            }

        }
        public static void Shift()
        {
            for (int i = 0; i < Game1.n; ++i)
            {
                for (int j = 0; j < Game1.n; ++j)
                {
                    Game1.blocks[i, j].position.X += Game1.speed;
                }
            }

        }
        public static void Game()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if ((lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            {
                for (int i = 0; i < Game1.n; ++i)
                {
                    for (int j = 0; j < Game1.n; ++j)
                    {
                        if (Game1.blocks[i, j].GetRec().Contains(currentMouseState.X, currentMouseState.Y))
                        {
                            if (mouseStat == 0)
                            {
                                Game1.blocks[i, j].active = true;
                                mouseStat = 1;
                                lastI = i;
                                lastj = j;
                            }
                            else
                            {
                                mouseStat = 0;
                                if (((i == lastI + 1) && (j == lastj)) ||
                                        ((i == lastI - 1) && (j == lastj)) ||
                                        ((i == lastI) && (j == lastj + 1)) ||
                                        ((i == lastI) && (j == lastj - 1)))
                                {
                                    Operators.SwapBlock(Game1.blocks[lastI, lastj], Game1.blocks[i, j]);
                                    if (Operators.Check())
                                    {
                                        while(Operators.Check())
                                            Operators.DelBlocks();
                                    }
                                    else
                                        Operators.SwapBlock(Game1.blocks[i, j], Game1.blocks[lastI, lastj]);
                                }
                                Game1.blocks[lastI, lastj].active = false;
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
                Rectangle rec = new Rectangle(Game1.graphics.PreferredBackBufferWidth / 2 - Game1.ok.Width / 2, Game1.graphics.PreferredBackBufferHeight / 2 - Game1.ok.Height / 2 + 200, Game1.ok.Width, Game1.ok.Height);
                if (rec.Contains(currentMouseState.X, currentMouseState.Y))
                {
                    if (Game1.score > Game1.highScore)
                    {
                        Reader.SetHighScore(Game1.fullPath);
                        Reader.GetHighScore(Game1.fullPath);
                    }
                    Game1.score = 0;
                    Game1.delay = 61;
                    Game1.mode = "Menu";
                }
            }
        }
    }
}
