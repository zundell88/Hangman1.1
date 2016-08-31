using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman1._1
{
    public class Player
    {
        int playerScore;
        int playerLife;
        public string PlayerName { get; set ; }
        public int Life
        {
            get { return playerLife; }
            private set { playerLife = value; }
        }
        public int Score
        {
            get { return playerScore;}
            private set { playerScore = value; }
        }
        public Player()
        {
            playerScore = 0;
            playerLife = 0;
        }
        public void LifeLoss() => playerLife--;
        public void AddScore(int point) => playerScore += point;
        public void RemoveScore(int point) => playerScore -= point;
        public void ResetPlayerLife() => playerLife = 10;
        public void ResetPlayerScore() => playerScore = 0;
    }
}
