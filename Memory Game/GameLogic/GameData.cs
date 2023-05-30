using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.GameLogic
{
    public class GameData
    {
        public GameBoard Board { get; set; }
        public GameLevel Level { get; set; }
        public GameData()
        {
            
        }
        public GameData(GameBoard board)
        {
            Board = new GameBoard();
            Level = new GameLevel();
            Board.GenerateBoard();
        }
        public GameData(GameBoard board, GameLevel level)
        {
            Board = board;
            Level = level;
        }
    }
}
