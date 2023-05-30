using Memory_Game.Pages.LittleWindows;
using Memory_Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfXMLSerialization.MyClasses;

namespace Memory_Game
{
    public partial class Login : Page
    {
        private int _selectedUserIndex;
        private int _selectedAvatarIndex;
        private List<Avatar> _avatars;
        const int numberOfAvatars = 9;
        public MainWindow mainWindow;
        public UserVM userVM;
        public SerializationActions<User> usersSerializationAct;
        public ObjectToSerialize<User> objectToSerialize;
        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            userVM = new UserVM();
            usersSerializationAct = new SerializationActions<User>(userVM.Users);
            objectToSerialize = new ObjectToSerialize<User>();

            //objectToSerialize.ObjectsToSerializeCollection.Add(new User("asd", "asd"));
            //usersSerializationAct.SerializeObject(objectToSerialize, "../../../SavedInfo/credentials.xml");
            objectToSerialize.ObjectsToSerializeCollection = usersSerializationAct.DeserializeObject("../../../SavedInfo/credentials.xml");
            userVM.Users = objectToSerialize.ObjectsToSerializeCollection;
            _selectedUserIndex = -1;

            _avatars = new List<Avatar>();
            for (int i = 1; i <= numberOfAvatars; i++)
            {
                _avatars.Add(new Avatar($"../Avatars/{i}.png"));
            }
            _selectedAvatarIndex = 0;

            // Set data context
            DataContext = this;
            UserList.DataContext = userVM;
        }

        public void RefreshUsers()
        {
            userVM.Users.Clear();
            userVM.Users = usersSerializationAct.DeserializeObject("../../../SavedInfo/credentials.xml");
        }

        public ObservableCollection<User> Users
        {
            get { return userVM.Users; }
        }

        public void AddUser(User user)
        {
            userVM.Users.Add(user);
        }

        public User SelectedUser
        {
            get { return _selectedUserIndex >= 0 ? userVM.Users[_selectedUserIndex] : null; }
        }

        public List<Avatar> Avatars
        {
            get { return _avatars; }
        }

        public Avatar SelectedAvatar
        {
            get { return _avatars[_selectedAvatarIndex]; }
        }

        private void User_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUserIndex = UserList.SelectedIndex;
        }

        private void NextAvatar_Click(object sender, RoutedEventArgs e)
        {
            _selectedAvatarIndex = (_selectedAvatarIndex + 1) % _avatars.Count;
            AvatarViewer.Source = new BitmapImage(new Uri(SelectedAvatar.ImageFile, UriKind.Relative));
        }

        private void PreviousAvatar_Click(object sender, RoutedEventArgs e)
        {
            _selectedAvatarIndex = (_selectedAvatarIndex - 1 + _avatars.Count) % _avatars.Count;
            AvatarViewer.Source = new BitmapImage(new Uri(SelectedAvatar.ImageFile, UriKind.Relative));
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.NavigationService.Navigate(new CreateUser(this));
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to permanently delete this user?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                objectToSerialize.ObjectsToSerializeCollection.Remove(SelectedUser);
                usersSerializationAct.SerializeObject(objectToSerialize, "../../../SavedInfo/credentials.xml");
                RefreshUsers();
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            var authWindow = new Authentication(SelectedUser);
            authWindow.Owner = mainWindow;

            bool? result = authWindow.ShowDialog();

            if (result == true && SelectedUser != null &&SelectedUser.IsAuthenticated)
            {
                SelectedUser.Avatar = SelectedAvatar;
                mainWindow.MainFrame.NavigationService.Navigate(new PlayPage(mainWindow, SelectedUser, objectToSerialize));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
