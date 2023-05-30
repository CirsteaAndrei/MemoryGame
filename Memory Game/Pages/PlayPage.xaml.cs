using Memory_Game.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfXMLSerialization.MyClasses;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {
        MainWindow mainWindow;
        User currentUser;
        ObjectToSerialize<User> _objectToSerialize;
        public PlayPage(MainWindow mainWindow, User user, ObjectToSerialize<User>objectToSerialize)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            currentUser = user;
            _objectToSerialize = objectToSerialize;
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.NavigationService.Navigate(new Game(currentUser,_objectToSerialize));
        }

        private void LoadGameBtnClicked(object sender, RoutedEventArgs e)
        {
            if(currentUser.GameData!=null)
            {
                MessageBox.Show("savedata found");
            }
            mainWindow.MainFrame.NavigationService.Navigate(new Game(currentUser, _objectToSerialize, currentUser.GameData));
        }
    }
}
