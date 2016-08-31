using System;
using System.IO;
using System.Linq;

namespace Hangman1._1
{
    public class ConsolePresenter
    {
        public static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var bannerString = "-## [HANGMAN 1.1] ##-\n\n";
            var stringToPrint = StringUtils.PrintTextInCenter(bannerString);
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }
        public static void PrintWelcome() => Console.WriteLine(StringUtils.PrintTextInCenter("Välkommen till hänga-gubbe-spelet!\n"));
        public static void PrintRules()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(StringUtils.PrintTextInCenter("[Regler]"));
            Console.ResetColor();
            Console.WriteLine(StringUtils.PrintTextInCenter("  Spelet går ut på att försöka lista ut ett hemligt ord, genom att chansa på en bokstav (A -Ö)\n   man tror kan finnas i ordet. Spelaren har 10 chansingar på sig varje omgång och om man inte\n      lyckas lista ut det hemliga ordet under dessa, hängs man och spelet är förlorat.\n"));
        }
        public static string GetPlayerName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(StringUtils.PrintTextInCenter("[Skapa spelare]"));
            Console.ResetColor();
            Console.Write(StringUtils.PrintTextInCenter("Skriv in spelarnamn: "));
            var playerName = Console.ReadLine();
            Console.Clear();
            return playerName;
        }
        public static int GetDifficilty()
        {
            bool madeChoice = false;
            int diffChoice;
            do
            {
                Console.Clear();
                PrintBanner();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(StringUtils.PrintTextInCenter("[Välj svårighet]\n"));
                Console.ResetColor();
                Console.WriteLine(StringUtils.PrintTextInCenter("(1) LÄTT "));
                Console.WriteLine(StringUtils.PrintTextInCenter("(2) MEDEL"));
                Console.WriteLine(StringUtils.PrintTextInCenter("(3) SVÅR"));
                Console.WriteLine();
                Console.Write(StringUtils.PrintTextInCenter("Mitt val: "));
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        diffChoice = 1;
                        madeChoice = true;
                        break;
                    case "2":
                        diffChoice = 2;
                        madeChoice = true;
                        break;
                    case "3":
                        diffChoice = 3;
                        madeChoice = true;
                        break;
                    default:
                        diffChoice = 0;
                        Console.Write(StringUtils.PrintTextInCenter("Ogiltigt val, försök igen"));
                        Console.ReadLine();
                        break;
                }
            } while (madeChoice != true);
            return diffChoice;
        }
        public static void DrawGame(Player player, string secretWord, string shownedWord)
        {
            Console.Clear();
            PrintBanner();
            var hangman =
                File.ReadAllText("D:/Christofer/Documents/Visual Studio 2015/Projects/Hangman1.1/Hangman1.1/gubbe/" +
                                   (10 - player.Life) + ".txt");
            Console.WriteLine(StringUtils.PrintTextInCenter(hangman));
            Console.WriteLine("\n\n\n");
            ConsoleColor color;
            if (player.Life >= 7)
                color = ConsoleColor.Green;
            else if(player.Life < 7 && player.Life >= 4)
                color = ConsoleColor.Yellow;
            else
                color = ConsoleColor.Red;

            Console.ForegroundColor = color;
            Console.WriteLine(StringUtils.PrintTextInCenter($"Du har {player.Life}/10 försök kvar"));
            Console.ResetColor();
            Console.WriteLine(StringUtils.PrintTextInCenter("--Dolda ordet--\n"));
            Console.WriteLine(StringUtils.PrintTextInCenter($"[{shownedWord}]\n"));
        }
        public static char AskForLetter()
        {
            string guess;
            var used = Word.listOfUsedLetters.Aggregate("", (current, letter) => current + (letter.ToString() + ", "));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(StringUtils.PrintTextInCenter($"[Använda bokstäver]"));
            Console.ResetColor();
            Console.Write(StringUtils.PrintTextInCenter($"{used}"));
            Console.WriteLine();
            do
            {
                Console.Write(StringUtils.PrintTextInCenter("Välj en bokstav: "));
                guess = Console.ReadLine()?.ToUpper();

            } while (guess =="");
            var tryedLetter = guess[0];
            return tryedLetter;
        }
        public static bool AskPlayAgain()
        {
            var playAgain = false;
            var once = true;
            var keepAsking = true;

            while (keepAsking)
            {
                string decision;
                if (once)
                {   
                    Console.WriteLine();
                    Console.Write(StringUtils.PrintTextInCenter("Vill du spela igen, och kanske samla på dig fler poäng? [ Y / N ]: "));
                    decision = Console.ReadLine()?.ToLower();
                    once = false;
                }
                else
                {
                    Console.Write(StringUtils.PrintTextInCenter("Tryck (Y) för att fortsätta, och (N) för att sluta spela: "));
                    decision = Console.ReadLine()?.ToLower();
                }

                if (decision == "y" || decision == "")
                {
                    playAgain = true;
                    keepAsking = false;
                    Console.Clear();
                    PrintBanner();

                }
                else if (decision == "n")
                {
                    keepAsking = false;
                    Console.Clear();
                    PrintBanner();
                }
            } 
            return playAgain;
        }
        public static void PrintSecretWord(string secretWord, int playerScore)
        {
            Console.Clear();
            var gameOver =
                File.ReadAllText(
                    "D:/Christofer/Documents/Visual Studio 2015/Projects/Hangman1.1/Hangman1.1/gubbe/gameover.txt");
            Console.WriteLine(gameOver);
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(StringUtils.PrintTextInCenter("Det hemliga ordet var : ", Word.listOfUsedLetters.Count * -1));
            Console.ResetColor();
            foreach (char letter in secretWord)
            {
                Console.Write($"{letter} ");
            }
            Console.WriteLine();
            Console.WriteLine(StringUtils.PrintTextInCenter($"Din slutliga poäng blev : {playerScore}"));
        }
        public static void DrawWinning(string secretWord, string playerName, int playerScore)
        {
            Console.Clear();
            PrintBanner();
            var runMan =
                File.ReadAllText(
                    "D:/Christofer/Documents/Visual Studio 2015/Projects/Hangman1.1/Hangman1.1/gubbe/winner.txt");
            Console.WriteLine(runMan);
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(StringUtils.PrintTextInCenter("Det hemliga ordet var: ", Word.listOfUsedLetters.Count * -1));
            Console.ResetColor();
            foreach (char letter in secretWord)
            {
                Console.Write($"{letter} ");
            }
            Console.WriteLine();
            Console.WriteLine(StringUtils.PrintTextInCenter($"Din nuvarande poäng är : {playerScore}"));
        }
        public static void DrawGoodbye()
        {
            Console.Clear();
            PrintBanner();
            Console.WriteLine(StringUtils.PrintTextInCenter("Tack för att du spelade, välkommen tillbaka!"));
        }
        public static void PrintAlternative(Highscore highscore)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(StringUtils.PrintTextInCenter("[Alternativ]"));
            Console.ResetColor();
            Console.WriteLine(StringUtils.PrintTextInCenter("  [Enter] - för att börja spela"));
            Console.WriteLine(StringUtils.PrintTextInCenter("[H] - för att se Highscore"));
            Console.WriteLine(StringUtils.PrintTextInCenter("  [Q] - för att avsluta spelet"));
            Console.Write(StringUtils.PrintTextInCenter("Val: "));
            var val = Console.ReadLine()?.ToLower();
            switch (val)
            {
                case "h":
                    highscore.PrintHighscore();
                    Game.GameStartUp();
                    break;
                case "q":
                    DrawGoodbye();
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                default:
                    Game.SetupGame();
                    break;
            }
        }
    }
}
