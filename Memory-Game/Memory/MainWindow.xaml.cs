using System;
using System.Collections;
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
            InitializeComponent();
            var gameBoard = Matrix.Make(Card.Shuffled(Constant.AllUnique));
            Matrix.TraceBoard(gameBoard);
            var rootGrid = new Grid {Name = "RootGrid"};

            //var hoofdmenu = new Grid();
            //rootGrid.Children.Add(hoofdmenu);
            var cardGrid = FillCardGrid(gameBoard, GenerateCardGrid());
            rootGrid.Children.Add(cardGrid);
            MWindow.Content = rootGrid;

            RevolveCard(0,3,gameBoard, cardGrid);
        }
        /// <summary>
        /// Revolves card in both
        /// </summary>
        /// <param name="row">row in gameboard</param>
        /// <param name="column">column in gameboard</param>
        /// <param name="gameBoard">gameboard to revolve card in</param>
        /// <param name="cardGrid">UI element to revolve card in</param>
        private void RevolveCard(int row, int column, Hashtable[,] gameBoard, Grid cardGrid)
        {
            Image x;
            if ((bool) gameBoard[row, column]["Flipped"])
            {
                gameBoard[row, column]["Flipped"] = false;
                x = CreateImage(gameBoard[row, column], "Back");
            }
            else
            {
                gameBoard[row, column]["Flipped"] = true;
                x = CreateImage(gameBoard[row, column], "Front");
            }


            cardGrid.Children.Add(x);
            cardGrid.Children.Remove(GetGridElement(cardGrid,row, column));
            Grid.SetRow(x, row);
            Grid.SetColumn(x, column);


        }

        UIElement GetGridElement(Grid g, int r, int c)
        {
            for (int i = 0; i < g.Children.Count; i++)
            {
                UIElement e = g.Children[i];
                if (Grid.GetRow(e) == r && Grid.GetColumn(e) == c)
                    return e;
            }
            return null;
        }

        /// <summary>
        ///     Takes card matrix maps its images onto the GUI
        /// </summary>
        /// <param name="matrix">Representation of gameboard in 2d array of hashtables</param>
        /// <param name="cardGrid">Grid to be filled with Images</param>
        /// <returns>New modified grid</returns>
        private static Grid FillCardGrid(Hashtable[,] matrix, Grid cardGrid)
        {
            var newGrid = cardGrid;
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
            {
                var x = CreateImage(matrix[i, j], "Back");
                newGrid.Children.Add(x);
                Grid.SetRow(x, j);
                Grid.SetColumn(x, i);
            }

            return newGrid;
        }

        /// <summary>
        ///     Add x amount of columns to a grid, MUTATES the grid
        /// </summary>
        /// <param name="grid">Grid to add columns to</param>
        /// <param name="amount">Amount of columns to be added</param>
        private static void AddColumns(Grid grid, int amount = 1)
        {
            for (var i = 0; i < amount; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        /// <summary>
        ///     Add x amount of rows to a grid, MUTATES the grid
        /// </summary>
        /// <param name="grid">Grid to add rows to</param>
        /// <param name="amount">Amount of rows to be added</param>
        private static void AddRows(Grid grid, int amount = 1)
        {
            for (var i = 0; i < amount; i++)
                grid.RowDefinitions.Add(new RowDefinition());
        }

        /// <summary>
        ///     Generates gameplay screen dynamically
        /// </summary>
        /// <returns>New grid</returns>
        private Grid GenerateCardGrid()
        {
            var cardGrid = new Grid {Name = "CardGrid", ShowGridLines = true};

            AddColumns(cardGrid, 4);
            AddRows(cardGrid, 4);

            return cardGrid;
        }

        /// <summary>
        ///     Returns Image based on the cardNumber
        /// </summary>
        /// <param name="card"></param>
        /// <param name="side">either back or front</param>
        /// <returns>new image</returns>
        private static Image CreateImage(IDictionary card, string side)
        {
            var simpleImage = new Image {Width = 200, Margin = new Thickness(5)};

            var bi = new BitmapImage();

            bi.BeginInit();

            bi.UriSource = (Uri) card[side];
            //TODO: make it so that the imgs are copied to compiled folder

            bi.EndInit();

            simpleImage.Source = bi;
            return simpleImage;
        }
    }
}