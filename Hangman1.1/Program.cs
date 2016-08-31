using System;

namespace Hangman1._1
{
    class Program
    {
        private static bool playGame = true;
        
        static void Main(string[] args)
        {
            Console.Title = "Hangman 1.1 CCS Games";
            while (playGame)
            {
                Game.GameStartUp();
                bool gameContinues = true;
                while (gameContinues)
                {
                    Game.PlayGame();
                    gameContinues = ConsolePresenter.AskPlayAgain();
                }
                Game.SaveHighscore();
            }
        }
    }
}
