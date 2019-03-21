using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotlight
{
    public static class Operators
    {
        public static void FillBlocks()
        {
            for (int i = 0; i < Game1.N; ++i)
            {
                for (int j = 0; j < Game1.N; ++j)
                {
                    Game1.Blokcs[i, j].Sprite = Game1.light;
                }
            }
        }
        public static void FullSwap(int i, int j)
        {
            Swap(i, j);
            if (i + 1 != Game1.N)
            {
                Swap(i + 1, j);
            }
            if (i - 1 != -1)
            {
                Swap(i - 1, j);
            }
            if (j + 1 != Game1.N)
            {
                Swap(i, j + 1);
            }
            if (j - 1 != -1)
            {
                Swap(i, j - 1);
            }
        }
        public static void Step(int Value)
        {
            Random rand = new Random((int)(DateTime.Now.Ticks) + Value);
            int i = rand.Next(Game1.N);
            int j = rand.Next(Game1.N);
            FullSwap(i, j);
        }
        public static void Swap(int i, int j)
        {
            if (Game1.Blokcs[i, j].Sprite == Game1.light)
            {
                Game1.Blokcs[i, j].Sprite = Game1.dark;
            }
            else
            {
                Game1.Blokcs[i, j].Sprite = Game1.light;
            }
        }
        public static bool CheckBlock()
        {
            for (int i = 0; i < Game1.N; ++i)
            {
                for (int j = 0; j < Game1.N; ++j)
                {
                    if (Game1.Blokcs[i, j].Sprite == Game1.dark)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
