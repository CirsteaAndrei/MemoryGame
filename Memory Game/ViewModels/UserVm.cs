using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.ViewModels
{
    public class UserVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this,
                new PropertyChangedEventArgs(propertyName));
        }
        private User myUser;
        public User MyUser
        {
            get
            {
                return myUser;
            }
            set
            {
                myUser = value;
                NotifyPropertyChanged("MyUser");
            }
        }
        public ObservableCollection<User> Users { get; set; }
        public UserVM()
        {
            
            string Username = "ana";
            string Password = "123";
            MyUser = new User(Username, Password);
            Users = new ObservableCollection<User>()
            {
                new User("ion", "123"),
                new User("test", "pass")
            };
        }
    }
}
