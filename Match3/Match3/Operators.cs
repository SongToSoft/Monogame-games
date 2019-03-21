using Match3.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    //Класс для преобразований массива с блоками
    static class Operators
    {
        private static Random rand;
        private static bool Checker = false;
        //Генерация поля без совпадающих блоков
        public static void Generate()
        {
            while (Check())
            {
                Checker = true;
                rand = new Random();
                for (int i = 0; i < Game1.N; ++i)
                {
                    for (int j = 0; j < Game1.N; j++)
                    {
                        Game1.Blocks[i, j] = new Block(150 + 60 * i, 150 + 60 * j, rand.Next(0, 5));
                    }
                }
            }
        }
        //Проверка поля на совпадения
        public static bool Check()
        {
            if (Checker == true)
            {
                for (int i = 0; i < 5; ++i)
                    RoundBlocks(i);
                for (int i = 0; i < Game1.N; ++i)
                {
                    for (int j = 0; j < Game1.N; ++j)
                    {
                        if (Game1.Blocks[i, j].Del == true)
                            return true;
                    }
                }
                return false;
            }
            return true;
        }
        public static void SwapBlock(Block One, Block Two)
        {
            int Stat = One.Status;
            Texture2D Sprite = One.Sprite;
            bool Del = One.Del;
            One.Status = Two.Status;
            One.Del = Two.Del;
            One.Sprite = Two.Sprite;
            Two.Status = Stat;
            Two.Del = Del;
            Two.Sprite = Sprite;
        }
        public static void DelBlocks()
        {
            //Поднимаем все блоки для удаления
            for (int i = Game1.N - 1; i > 0; --i)
            {
                for (int j = Game1.N - 1; j > 0; --j)
                {
                    if (Game1.Blocks[i, j].Del == true)
                    {
                        SwapBlock(Game1.Blocks[i, j], Game1.Blocks[i - 1, j]);
                    }
                }
            }
            //Заменяем удаленные блоки на новые
            for (int i = 0; i < Game1.N; ++i)
            {
                for (int j = 0; j < Game1.N; ++j)
                {
                    if (Game1.Blocks[i, j].Del == true)
                    {
                        Game1.Blocks[i, j].Status = rand.Next(0, 5);
                        Game1.Blocks[i, j].Sprite = SetSprite(Game1.Blocks[i, j].Status);
                        Game1.Blocks[i, j].Del = false;
                        ++Game1.Score;
                    }
                }
            }
        }
        public static Texture2D SetSprite(int Status)
        {
            Texture2D sprite = Game1.RedSprite;
            if (Status == 0)
                sprite = Game1.RedSprite;
            if (Status == 1)
                sprite = Game1.BlueSprite;
            if (Status == 2)
                sprite = Game1.GreenSprite;
            if (Status == 3)
                sprite = Game1.OrangeSprite;
            if (Status == 4)
                sprite = Game1.PinkSprite;
            return sprite;
        }
        //Обход блоков и отметка их на удаление
        public static void RoundBlocks(int Stat)
        {
            int Count = 0;
            for (int i = 0; i < Game1.N; ++i)
            {
                for (int j = 0; j < Game1.N; ++j)
                {
                    if (Game1.Blocks[i, j].Status == Stat)
                    {
                        ++Count;
                    }
                    else
                    {
                        if (Count > 2)
                        {
                            for (int k = (j - Count); k < j; ++k)
                            {
                                Game1.Blocks[i, k].Del = true;
                            }
                        }
                        Count = 0;
                    }
                }
                if (Count > 2)
                {
                    for (int k = (Game1.N - Count); k < Game1.N; ++k)
                    {
                        Game1.Blocks[i, k].Del = true;
                    }
                }
                Count = 0;
            }
            Count = 0;
            for (int j = 0; j < Game1.N; ++j)
            {
                for (int i = 0; i < Game1.N; ++i)
                {
                    if (Game1.Blocks[i, j].Status == Stat)
                    {
                        ++Count;
                    }
                    else
                    {
                        if (Count > 2)
                        {
                            for (int k = (i - Count); k < i; ++k)
                            {
                                Game1.Blocks[k, j].Del = true;
                            }
                        }
                        Count = 0;
                    }
                }
                if (Count > 2)
                {
                    for (int k = (Game1.N - Count); k < Game1.N; ++k)
                    {
                        Game1.Blocks[k, j].Del = true;
                    }
                }
                Count = 0;
            }
        }
    }
}