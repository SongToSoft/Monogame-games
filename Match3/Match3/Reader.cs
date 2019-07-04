using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    //Класс для считывания и записи лучшего результата
    static class Reader
    {
        static public void GetHighScore(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string line = sr.ReadLine();
            if (line != null)
            {
                string[] numbers = line.Split(' ');
                Game1.highScore = int.Parse(numbers[0]);
            }
            sr.Close();
        }
        static public void SetHighScore(string Path)
        {
            StreamWriter sw = new StreamWriter(Path);
            sw.Write(Game1.score);
            sw.Close();
        }
    }
}
