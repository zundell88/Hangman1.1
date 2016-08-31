using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hangman1._1
{
    public class Highscore
    {
        public string PlayerName { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }

        public string pathString ="D:/Christofer/Documents/Visual Studio 2015/Projects/Hangman1.1/Hangman1.1/Highscore/Highscore.txt";

        public Highscore(string data)
        {
            var d = data.Split(' ');
            PlayerName = d[0];

            int num;
            if (int.TryParse(d[1], out num))
                Score = num;
        }
        public override string ToString() => $"{Position}. {PlayerName}: Score = {Score}";
        public List<Highscore> ReadScoreFromFile(string path)
        {
            var scores = new List<Highscore>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    scores.Add(new Highscore(line));
                }
            }
            return SortAndPositionHighscores(scores);
        }
        public List<Highscore> SortAndPositionHighscores(List<Highscore> scores)
        {
            scores = scores.OrderByDescending(s => s.Score).ToList();
            int pos = 1;
            scores.ForEach(s => s.Position = pos++);
            return scores.ToList();
        }
        public void SavePlayersHighscore()
        {
            string text = $"{PlayerName} {Score}";
            List<string> scoreList;
            if (File.Exists(pathString))
                scoreList = File.ReadAllLines(pathString).ToList();
            else
                scoreList = new List<string>();

            scoreList.Add(text);
            File.WriteAllLines(pathString, scoreList);
        }

        public void PrintHighscore()
        {
            Console.Clear();
            var highscores = ReadScoreFromFile(pathString);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(StringUtils.PrintTextInCenter("**HIGHSCORES**"));
            Console.ResetColor();
            foreach (var score in highscores)
            {
                Console.WriteLine();
                string scores = score.ToString();
                Console.WriteLine(StringUtils.PrintTextInCenter(scores));
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
