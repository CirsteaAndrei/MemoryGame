using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memory_Game.Pages.LittleWindows
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        User _selectedUser;
        public Authentication(User selectedUser)
        {
            InitializeComponent();
            _selectedUser = selectedUser;
            ShowUsername.Content = selectedUser.Name;
        }

        public void ShowDialogAndLogin()
        {
            

            this.ShowDialog();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string password = Password.Password;

            // check if the username and password are valid
            bool isValid = ValidateCredentials(password);

            // if the username and password are valid
            if (isValid)
            {
                // set the IsAuthenticated property of the player object
                _selectedUser.IsAuthenticated = true;

                // close the authentication window
                DialogResult = true;
            }
            else
            {
                // display an error message
                MessageBox.Show("Invalid password.");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool ValidateCredentials(string password)
        {
            if(_selectedUser.Password!=password)
            {
                return false;
            }
            return true;
        }
    }
}
