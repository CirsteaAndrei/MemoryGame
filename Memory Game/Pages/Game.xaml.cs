using Memory_Game.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Memory_Game.Pages
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private Tile? _firstSelectedTile;
        private Button? _firstSelectedTileButton;
        private Tile? _secondSelectedTile;
        private Button? _secondSelectedTileButton;

        public GameData _gameData;
        public User _currentUser { get; set; }
        private ObjectToSerialize<User> _objectToSerialize;
        SerializationActions<User> _serializationActions;
        public Game(User user, ObjectToSerialize<User> objectToSerialize)
        {
            InitializeComponent();
            _firstSelectedTile = null;
            _firstSelectedTileButton = null;
            _secondSelectedTile = null;
            _secondSelectedTileButton = null;
            _currentUser = user;
            _gameData = new GameData(new GameBoard());
            this.DataContext = this;
            MistakesMadeLabel.Content = $"Mistakes made:\n {_gameData.Level.MistakesMade}/{_gameData.Level.MistakesAllowed}";
            _objectToSerialize = objectToSerialize;
            _serializationActions = new SerializationActions<User>(_objectToSerialize.ObjectsToSerializeCollection);
            TilesItemControl.DataContext = _gameData;
        }

        public Game(User user, ObjectToSerialize<User> objectToSerialize, GameData gameData)
        {
            InitializeComponent();
            this.DataContext = this;
            _firstSelectedTile = null;
            _secondSelectedTile = null;
            _currentUser = user;
            if (gameData != null)
            {
                _gameData = user.GameData;
            }
            else
            {
                MessageBox.Show("No saved game data found, new game started");
                _gameData = new GameData();
            }
            
            MistakesMadeLabel.Content = $"Mistakes made:\n {_gameData.Level.MistakesMade}/{_gameData.Level.MistakesAllowed}";
            _objectToSerialize = objectToSerialize;
            _serializationActions = new SerializationActions<User>(_objectToSerialize.ObjectsToSerializeCollection);
            TilesItemControl.DataContext = _gameData;
        }

        public Avatar UserAvatar
        { get { return _currentUser.Avatar; } }

        private void OnTileClicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var tile = (Tile)button.DataContext;

            // don't allow selecting the same tile twice
            if (tile == _firstSelectedTile || tile.IsMatched)
            {
                return;
            }

            if (_firstSelectedTile == null)
            {
                // first tile selected
                _firstSelectedTile = tile;
                _firstSelectedTileButton = button;
                ShowTile(tile, _firstSelectedTileButton);
            }
            else if (_secondSelectedTile == null)
            {
                // second tile selected
                _secondSelectedTile = tile;
                _secondSelectedTileButton = button;
                ShowTile(tile, _secondSelectedTileButton);

                // check for match
                if (_firstSelectedTile.ImageFile == _secondSelectedTile.ImageFile)
                {
                    _firstSelectedTile.IsMatched = true;
                    _secondSelectedTile.IsMatched = true;

                    _firstSelectedTile = null;
                    _secondSelectedTile = null;
                    _firstSelectedTileButton = null;
                    _secondSelectedTileButton = null;
                }
            }
            else
            {
                // third tile selected, reset the previous two
                HideTile(_firstSelectedTile, _firstSelectedTileButton);
                _firstSelectedTile = null;
                _firstSelectedTileButton = null;
                HideTile(_secondSelectedTile, _secondSelectedTileButton);
                _secondSelectedTile = null;
                _secondSelectedTileButton = null;
                _gameData.Level.MistakesMade++;
                MistakesMadeLabel.Content = $"Mistakes made:\n {_gameData.Level.MistakesMade}/{_gameData.Level.MistakesAllowed}";

                _firstSelectedTile = tile;
                _firstSelectedTileButton = button;
                ShowTile(tile, _firstSelectedTileButton);
            }
        }

        private void ShowTile(Tile tile, Button tileButton)
        {
            var button = tileButton;
            var grid = (Grid)button.Content;
            var frontImage = grid.Children.OfType<Image>().First(x => x.Name == "FrontImage");
            var backImage = grid.Children.OfType<Image>().First(x => x.Name == "BackImage");

            frontImage.Visibility = Visibility.Visible;
            backImage.Visibility = Visibility.Hidden;
        }

        private void HideTile(Tile tile, Button tileButton)
        {
            var button = tileButton;
            var grid = (Grid)button.Content;
            var frontImage = grid.Children.OfType<Image>().First(x => x.Name == "FrontImage");
            var backImage = grid.Children.OfType<Image>().First(x => x.Name == "BackImage");

            frontImage.Visibility = Visibility.Hidden;
            backImage.Visibility = Visibility.Visible;
        }

        private void SaveGameBtnClicked(object sender, RoutedEventArgs e)
        {
            int userIndex = _objectToSerialize.ObjectsToSerializeCollection.IndexOf(_currentUser);

            // Update the GameData object at that index with the new _gameData object
            _objectToSerialize.ObjectsToSerializeCollection[userIndex].GameData = _gameData;

            // Serialize the updated ObjectToSerialize<User> object and save it to disk
            _serializationActions.SerializeObject(_objectToSerialize, "../../../SavedInfo/credentials.xml");

            MessageBox.Show("Game saved successfully");
        }
    }

}
