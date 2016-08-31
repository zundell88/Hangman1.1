using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman1._1
{
    public class Word
    {
        private static string secretWord;
        private static string shownedWord;
        public static List<char> listOfUsedLetters = new List<char>();
        public string SecretWord => secretWord;
        public string ShownedWord => shownedWord;
        public static void SetDifficulty(int diffNum)
        {
            var fileName = "";
            switch (diffNum)
            {
                case 1:
                    fileName = "EasyWord.txt";
                    break;
                case 2:
                    fileName = "MediumWord.txt";
                    break;
                case 3:
                    fileName = "HardWord.txt";
                    break;
            }
            SetSecretWord(fileName);
        }

        private static void SetSecretWord(string fileName)
        {
            string[] listOfWords = File.ReadAllLines("D:/Christofer/Documents/Visual Studio 2015/Projects/Hangman1.1/Hangman1.1/Words/" + fileName);
            var rnd = new Random();
            var pick = rnd.Next(listOfWords.Length);
            secretWord = listOfWords[pick];
            SetShownedWord(secretWord);
            listOfUsedLetters.Clear();
        }

        private static void SetShownedWord(string secretword)
        {
            var wordLenght = secretword.Length;
            shownedWord = new string('-', wordLenght);
        }
        public static bool CheckLetter(char letterToCheck)
        {
            char[] shownedWordArray = shownedWord.ToCharArray();
            bool match = false;
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == letterToCheck)
                {
                    shownedWordArray[i] = secretWord[i];
                    match = true;
                }
                else
                {
                    if(!listOfUsedLetters.Contains(letterToCheck))
                    listOfUsedLetters.Add(letterToCheck);
                }
            }
            string word = "";
            for (int i = 0; i < shownedWordArray.Length; i++)
            {
                word += shownedWordArray[i];
            }
            shownedWord = word;
            return match;
        }
        public static bool WordComplete()
        {
            bool isComplete = shownedWord == secretWord;
            return isComplete;
        }
    }
}
