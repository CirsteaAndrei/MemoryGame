using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Memory_Game
{
    public class Tile
    {
        public string ImageFile { get; set; }
        public bool IsMatched { get; set; }
        public bool IsVisible { get; set; }
        public Button? AttachedButton { get; set; }

        public Tile(string file)
        {
            ImageFile = file;
            IsMatched = false;
            IsVisible = false;
            AttachedButton = null;
        }
    }
}
