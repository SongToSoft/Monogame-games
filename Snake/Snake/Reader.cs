using SnakeGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //Класс для считывания и записи лучшего результата
    static class Reader
    {
        static public void GetHighScore(string Path)
        {
            StreamReader SR = new StreamReader(Path);
            string line = SR.ReadLine();
            if (line != null)
            {
                string[] numbers = line.Split(' ');
                Game1.HighScore = int.Parse(numbers[0]);
            }
            SR.Close();
        }
        static public void SetHighScore(string Path)
        {
            StreamWriter SW = new StreamWriter(Path);
            SW.Write(Game1.Score);
            SW.Close();
        }
    }
}
