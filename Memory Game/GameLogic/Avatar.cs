using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    public class Avatar
    {
        public string ImageFile { get; set; }

        public Avatar(string imageFile)
        {
            ImageFile = imageFile;
        }
        public Avatar() { 
            ImageFile = string.Empty;
        }
    }
}
