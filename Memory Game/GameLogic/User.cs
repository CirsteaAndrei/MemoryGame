using Memory_Game.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public Avatar Avatar { get; set; }

        public bool IsAuthenticated { get; set; }

        public GameData? GameData;

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            IsAuthenticated = false;
            Avatar = new Avatar("../Avatars/1.png");
            GameData = null;
        }

        public User(){

        }
    }
}
