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
        private static bool checker = false;
        //Генерация поля без совпадающих блоков
        public static void Generate()
        {
            while (Check())
            {
                checker = true;
                rand = new Random();
                for (int i = 0; i < Game1.n; ++i)
                {
                    for (int j = 0; j < Game1.n; j++)
                    {
                        Game1.blocks[i, j] = new Block(150 + 60 * i, 150 + 60 * j, rand.Next(0, 5));
                    }
                }
            }
        }
        //Проверка поля на совпадения
        public static bool Check()
        {
            if (checker == true)
            {
                for (int i = 0; i < 5; ++i)
                    RoundBlocks(i);
                for (int i = 0; i < Game1.n; ++i)
                {
                    for (int j = 0; j < Game1.n; ++j)
                    {
                        if (Game1.blocks[i, j].del == true)
                            return true;
                    }
                }
                return false;
            }
            return true;
        }
        public static void SwapBlock(Block one, Block two)
        {
            int stat = one.status;
            Texture2D sprite = one.sprite;
            bool del = one.del;
            one.status = two.status;
            one.del = two.del;
            one.sprite = two.sprite;
            two.status = stat;
            two.del = del;
            two.sprite = sprite;
        }
        public static void DelBlocks()
        {
            //Поднимаем все блоки для удаления
            for (int i = Game1.n - 1; i > 0; --i)
            {
                for (int j = Game1.n - 1; j > 0; --j)
                {
                    if (Game1.blocks[i, j].del == true)
                    {
                        SwapBlock(Game1.blocks[i, j], Game1.blocks[i - 1, j]);
                    }
                }
            }
            //Заменяем удаленные блоки на новые
            for (int i = 0; i < Game1.n; ++i)
            {
                for (int j = 0; j < Game1.n; ++j)
                {
                    if (Game1.blocks[i, j].del == true)
                    {
                        Game1.blocks[i, j].status = rand.Next(0, 5);
                        Game1.blocks[i, j].sprite = SetSprite(Game1.blocks[i, j].status);
                        Game1.blocks[i, j].del = false;
                        ++Game1.score;
                    }
                }
            }
        }
        public static Texture2D SetSprite(int Status)
        {
            Texture2D sprite = Game1.redSprite;
            if (Status == 0)
                sprite = Game1.redSprite;
            if (Status == 1)
                sprite = Game1.blueSprite;
            if (Status == 2)
                sprite = Game1.greenSprite;
            if (Status == 3)
                sprite = Game1.orangeSprite;
            if (Status == 4)
                sprite = Game1.pinkSprite;
            return sprite;
        }
        //Обход блоков и отметка их на удаление
        public static void RoundBlocks(int Stat)
        {
            int Count = 0;
            for (int i = 0; i < Game1.n; ++i)
            {
                for (int j = 0; j < Game1.n; ++j)
                {
                    if (Game1.blocks[i, j].status == Stat)
                    {
                        ++Count;
                    }
                    else
                    {
                        if (Count > 2)
                        {
                            for (int k = (j - Count); k < j; ++k)
                            {
                                Game1.blocks[i, k].del = true;
                            }
                        }
                        Count = 0;
                    }
                }
                if (Count > 2)
                {
                    for (int k = (Game1.n - Count); k < Game1.n; ++k)
                    {
                        Game1.blocks[i, k].del = true;
                    }
                }
                Count = 0;
            }
            Count = 0;
            for (int j = 0; j < Game1.n; ++j)
            {
                for (int i = 0; i < Game1.n; ++i)
                {
                    if (Game1.blocks[i, j].status == Stat)
                    {
                        ++Count;
                    }
                    else
                    {
                        if (Count > 2)
                        {
                            for (int k = (i - Count); k < i; ++k)
                            {
                                Game1.blocks[k, j].del = true;
                            }
                        }
                        Count = 0;
                    }
                }
                if (Count > 2)
                {
                    for (int k = (Game1.n - Count); k < Game1.n; ++k)
                    {
                        Game1.blocks[k, j].del = true;
                    }
                }
                Count = 0;
            }
        }
    }
}