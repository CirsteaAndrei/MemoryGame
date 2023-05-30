using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.GameLogic
{
    public class GameLevel
    {
        public int LevelNr { get; set; }
        public int MistakesMade { get; set; }
        public int MistakesAllowed { get; set; }

        public GameLevel()
        {
            LevelNr = 1;
            MistakesMade = 0;
            setMistakesAllowed(LevelNr);
        }
        public void setMistakesAllowed(int LevelNr)
        {
            switch (LevelNr)
            {
                case 1:
                    MistakesAllowed = 40;
                    break;
                case 2:
                    MistakesAllowed = 30;
                    break;
                case 3:
                    MistakesAllowed = 20;
                    break;
                default:
                    MistakesAllowed = 15;
                    break;
            }
        }
    }
}
