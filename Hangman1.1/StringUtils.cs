using System;

namespace Hangman1._1
{
    class StringUtils
    {
        public static string PrintTextInCenter(string text, int padValue = 0)
        {
            var width = Console.WindowWidth;
            var padLenght = (width + text.Length) / 2;
            return text.PadLeft(padLenght + padValue, ' ');
        }
    }
}
