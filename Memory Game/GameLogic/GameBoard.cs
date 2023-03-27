using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    public class GameBoard
    {
        public List<List<Tile>> Images { get; set; }

        public GameBoard()
        {
            List<string> imagePaths = new List<string>();
            for (int i = 0; i < 13; i++)
            {
                imagePaths.Add($"../Images/GameImages/{i}.png");
                imagePaths.Add($"../Images/GameImages/{i}.png");
            }
            ShuffleList(imagePaths);

            Images = new List<List<Tile>>();
            int ctr = 0;
            for (int i = 0; i < 5; i++)
            {
                List<Tile> image = new List<Tile>();
                for (int j = 0; j < 5; j++)
                {
                    image.Add(new Tile( imagePaths[ctr]));
                    ctr++;
                }
                Images.Add(image);
            }
        }

        private static void ShuffleList<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    //public class GameBoard
    //{
    //    public ObservableCollection<ObservableCollection<Tile>> Images { get; set; }

    //    public GameBoard()
    //    {
    //        // Load all images into a list
    //        List<string> imageFiles = new List<string>();
    //        for (int i = 0; i < 13; i++)
    //        {
    //            imageFiles.Add($"../Images/GameImages/{i}.png");
    //            imageFiles.Add($"../Images/GameImages/{i}.png");
    //        }

    //        // Shuffle the list of image files
    //        Random random = new Random();
    //        for (int i = 0; i < imageFiles.Count; i++)
    //        {
    //            int j = random.Next(i, imageFiles.Count);
    //            string temp = imageFiles[i];
    //            imageFiles[i] = imageFiles[j];
    //            imageFiles[j] = temp;
    //        }

    //        // Initialize the images in the game board
    //        Images = new ObservableCollection<ObservableCollection<Tile>>();
    //        int index = 0;
    //        for (int row = 0; row < 4; row++)
    //        {
    //            ObservableCollection<Tile> imageRow = new ObservableCollection<Tile>();
    //            for (int col = 0; col < 4; col++)
    //            {
    //                imageRow.Add(new Tile(imageFiles[index]));
    //                index++;
    //            }
    //            Images.Add(imageRow);
    //        }
    //    }
    //}
}
