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

namespace Memory_Game.Pages
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private Tile? _firstSelectedTile;
        private Tile? _secondSelectedTile;
        int notMatchedCounter;
        public Game()
        {
            InitializeComponent();
            _firstSelectedTile = null;
            _secondSelectedTile = null;
            notMatchedCounter = 0;
        }

        private void OnTileClicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var tile = (Tile)button.DataContext;
            tile.AttachedButton = button;

            // don't allow selecting the same tile twice
            if (tile == _firstSelectedTile||tile.IsMatched)
            {
                return;
            }

            if (_firstSelectedTile == null)
            {
                // first tile selected
                _firstSelectedTile = tile;
                ShowTile(tile);
            }
            else if (_secondSelectedTile == null)
            {
                // second tile selected
                _secondSelectedTile = tile;
                ShowTile(tile);

                // check for match
                if (_firstSelectedTile.ImageFile == _secondSelectedTile.ImageFile)
                {
                    _firstSelectedTile.IsMatched = true;
                    _secondSelectedTile.IsMatched = true;

                    _firstSelectedTile = null;
                    _secondSelectedTile = null;
                }
            }
            else
            {
                // third tile selected, reset the previous two
                HideTile(_firstSelectedTile);
                _firstSelectedTile= null;
                HideTile(_secondSelectedTile);
                _secondSelectedTile= null;
                notMatchedCounter++;
                label.Content = notMatchedCounter;

                _firstSelectedTile = tile;
                ShowTile(tile);
            }
        }

        private void ShowTile(Tile tile)
        {
            var button = tile.AttachedButton;
            var grid = (Grid)button.Content;
            var frontImage = grid.Children.OfType<Image>().First(x => x.Name == "FrontImage");
            var backImage = grid.Children.OfType<Image>().First(x => x.Name == "BackImage");

            frontImage.Visibility = Visibility.Visible;
            backImage.Visibility = Visibility.Hidden;
        }

        private void HideTile(Tile tile)
        {
            var button = tile.AttachedButton;
            var grid = (Grid)button.Content;
            var frontImage = grid.Children.OfType<Image>().First(x => x.Name == "FrontImage");
            var backImage = grid.Children.OfType<Image>().First(x => x.Name == "BackImage");

            frontImage.Visibility = Visibility.Hidden;
            backImage.Visibility = Visibility.Visible;
        }

        //private Button FindButton(Tile tile)
        //{
        //    var itemsControl = (ItemsControl)VisualTreeHelper.GetParent(GetTileContainer(tile));
        //    var container = (FrameworkElement)itemsControl.ItemContainerGenerator.ContainerFromItem(tile);
        //    var button = (Button)container.ContentTemplate.FindName("TileButton", container);
        //    return button;
        //}

        private Panel GetTileContainer(Tile tile)
        {
            var itemsControl = (ItemsControl)VisualTreeHelper.GetParent(tile.AttachedButton);
            var container = (FrameworkElement)itemsControl.ItemContainerGenerator.ContainerFromItem(tile);
            return (Panel)VisualTreeHelper.GetParent(container);
        }
    }

}
