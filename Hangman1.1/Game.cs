using System;

namespace Hangman1._1
{
    public class Game
    {
        public static Player player = new Player();
        public static Word word = new Word();
        public static Highscore highscore = new Highscore($"{player.PlayerName} {player.Score}");

        public static void GameStartUp()
        {
            Console.Clear();
            ConsolePresenter.PrintBanner();
            ConsolePresenter.PrintWelcome();
            ConsolePresenter.PrintRules();
            ConsolePresenter.PrintAlternative(highscore);
           
        }
        public static void SetupGame()
        {
            Console.Clear();
            ConsolePresenter.PrintBanner();
            player.PlayerName = ConsolePresenter.GetPlayerName();
            highscore.PlayerName = player.PlayerName;
            player.ResetPlayerScore();
        }
        public static void PlayGame()
        {
            player.ResetPlayerLife();
            Word.SetDifficulty(ConsolePresenter.GetDifficilty());

            while (player.Life > 0)
            {
                ConsolePresenter.DrawGame(player, word.SecretWord, word.ShownedWord);
                if (Word.CheckLetter(ConsolePresenter.AskForLetter()))
                {
                    player.AddScore(3);
                }
                else
                {
                    player.RemoveScore(1);
                    player.LifeLoss();
                }
                if (Word.WordComplete())
                {
                    highscore.Score = player.Score;
                    ConsolePresenter.DrawWinning(word.SecretWord, player.PlayerName, player.Score);
                    Console.WriteLine();
                    break;
                }
            }
            if (player.Life <= 0)
            {
                highscore.Score = player.Score;
                ConsolePresenter.PrintSecretWord(word.SecretWord, player.Score);
            }
        }

        public static void SaveHighscore() => highscore.SavePlayersHighscore();
    }
}
