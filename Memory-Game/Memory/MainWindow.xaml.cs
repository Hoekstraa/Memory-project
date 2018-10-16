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
            var gameBoard = Matrix.Make(Card.Shuffled(Constant.AllUnique));
            Matrix.TraceBoard(gameBoard);

            InitPlayScreen();
            BoardToScreen(gameBoard);

            InitializeComponent();
        }


        /// <summary>
        ///     Takes array and maps it onto the GUI
        /// </summary>
        /// <param name="matrix"></param>
        private void BoardToScreen(Hashtable[,] matrix)
        {
            //
        }

        /// <summary>
        ///     Generates gameplay screen dynamically
        /// </summary>
        private void InitPlayScreen()
        {
            var mainWindow = new Window {Title = "Grid Sample"};

            var rootGrid = new Grid {Name = "RootGrid"};
            var cardGrid = new Grid {Name = "CardGrid", ShowGridLines = true};

            #region definitions

            var colDef1 = new ColumnDefinition();
            var colDef2 = new ColumnDefinition();
            var colDef3 = new ColumnDefinition();
            var colDef4 = new ColumnDefinition();
            cardGrid.ColumnDefinitions.Add(colDef1);
            cardGrid.ColumnDefinitions.Add(colDef2);
            cardGrid.ColumnDefinitions.Add(colDef3);
            cardGrid.ColumnDefinitions.Add(colDef4);

            var rowDef1 = new RowDefinition();
            var rowDef2 = new RowDefinition();
            var rowDef3 = new RowDefinition();
            var rowDef4 = new RowDefinition();
            cardGrid.RowDefinitions.Add(rowDef1);
            cardGrid.RowDefinitions.Add(rowDef2);
            cardGrid.RowDefinitions.Add(rowDef3);
            cardGrid.RowDefinitions.Add(rowDef4);

            #endregion

            for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                {
                    var x = CreateImage();
                    cardGrid.Children.Add(x);
                    Grid.SetRow(x, j);
                    Grid.SetColumn(x, i);
                }


            rootGrid.Children.Add(cardGrid);
            mainWindow.Content = rootGrid;
            mainWindow.Show();
            //RootGrid.Children.Add(cardGrid);
        }

        Image CreateImage()
        {
            var simpleImage = new Image {Width = 200, Margin = new Thickness(5)};

            var bi = new BitmapImage();

            bi.BeginInit();
            bi.UriSource = new Uri(@"C:\Users\Jan Hoekstra\Pictures\cats-yawn.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            simpleImage.Source = bi;
            return simpleImage;
        }
    }
}