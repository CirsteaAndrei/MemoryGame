using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
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
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Page
    {
        Login loginWindow;
        public CreateUser(Login loginWindow)
        {
            InitializeComponent();
            this.loginWindow = loginWindow;

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            loginWindow.objectToSerialize.ObjectsToSerializeCollection.Add(new User(Username.Text, Password.Password));
            loginWindow.usersSerializationAct.SerializeObject(loginWindow.objectToSerialize, "../../../SavedInfo/credentials.xml");
            NavigationService.GoBack();
            loginWindow.RefreshUsers();
            
        }
    }
}
