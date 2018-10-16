using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Memory
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Start of the program, makes calls to functions to:
        ///     make a new shuffled matrix * 6 and print them out 1 by 1.
        /// </summary>

        public MainWindow()
        {
            var gameBoard = Matrix.Make(Card.Shuffled(Constant.AllUnique));
            Matrix.TraceBoard(gameBoard);

            var mainWindow = new Window { Title = "GAME! :D" };
            var rootGrid = new Grid { Name = "RootGrid" };

            //var hoofdmenu = new Grid();
            //rootGrid.Children.Add(hoofdmenu);


            rootGrid.Children.Add(FillCardGrid(gameBoard, GenerateCardGrid()));
            mainWindow.Content = rootGrid;
            mainWindow.Show();

            InitializeComponent();
        }

        /// <summary>
        ///     Takes array and maps it onto the GUI
        /// </summary>
        /// <param name="matrix"></param>
        private Grid FillCardGrid(Hashtable[,] matrix, Grid cardGrid)
        {
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
            {
                var y = matrix[i, j]["Number"];
                Trace.WriteLine(y.GetType());
                var x = CreateImage(i + 1);
                cardGrid.Children.Add(x);
                Grid.SetRow(x, j);
                Grid.SetColumn(x, i);
            }
            return cardGrid;
        }

        private void AddColumns(Grid grid, int amount)
        {
            for(int i = 0; i < amount; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void AddRows(Grid grid, int amount)
        {
            for (int i = 0; i < amount; i++)
                grid.RowDefinitions.Add(new RowDefinition());
        }

        /// <summary>
        ///     Generates gameplay screen dynamically
        /// </summary>
        private Grid GenerateCardGrid()
        {
            var cardGrid = new Grid {Name = "CardGrid", ShowGridLines = true};

            AddColumns(cardGrid, 4);
            AddRows(cardGrid, 4);


            return cardGrid;
        }

        Image CreateImage(int number)
        {
            var simpleImage = new Image {Width = 200, Margin = new Thickness(5)};

            var bi = new BitmapImage();

            bi.BeginInit();

            bi.UriSource = new Uri
            (
                $"Images/{number}.png",
                UriKind.Relative
            );
            //TODO: change file Uri

            bi.EndInit();

            simpleImage.Source = bi;
            return simpleImage;
        }
    }
}