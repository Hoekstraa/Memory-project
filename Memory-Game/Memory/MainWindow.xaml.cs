using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Memory
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        ///     Variable containing x by y of card hashtables
        /// </summary>
        public static Hashtable[,] GameBoard;

        /// <summary>
        ///     Variable containing UI grid containing images.
        /// </summary>
        public static Grid CardGrid;

        /// <summary>
        ///     Start of the program, makes calls to functions to:
        ///     make a new shuffled matrix * 6 and print them out 1 by 1.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GameBoard = Matrix.Make(Card.Shuffled(Constant.AllUnique));
            Matrix.TraceBoard(GameBoard);

            var gameRootGrid = new Grid {Name = "GameRootGrid", ShowGridLines = true};
            var menuRootGrid = new Grid {Name = "MenuRootGrid", ShowGridLines = true};
            GeneratePlayGrid(gameRootGrid);
            GenerateMenuGrid(menuRootGrid);
            MWindow.Content = menuRootGrid; //Switch between menu and game screens
        }

        /// <summary>
        ///     Generates the grid the game is played in
        /// </summary>
        /// <param name="rootGrid">Grid to put playgrid into</param>
        private static void GeneratePlayGrid(Grid rootGrid)
        {
            var playingInfo = new Grid {Name = "PlayingInfo", ShowGridLines = true};

            CardGrid = GameLogic.FillCardGrid(GameBoard, GameLogic.GenerateCardGrid());

            var col2 = new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)};
            var col1 = new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)};

            rootGrid.ColumnDefinitions.Add(col2);
            rootGrid.ColumnDefinitions.Add(col1);
            rootGrid.Children.Add(CardGrid);

            rootGrid.Children.Add(playingInfo);
            Grid.SetColumn(playingInfo, 1);
        }

        /// <summary>
        ///     Generates the main menu
        /// </summary>
        /// <param name="rootGrid"></param>
        private static void GenerateMenuGrid(Grid rootGrid)
        {
            var col2 = new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)}; // 2/3 of screen
            var col1 = new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}; // 1/3 of screen

            rootGrid.ColumnDefinitions.Add(col2);
            rootGrid.ColumnDefinitions.Add(col1);

            var menuButtonGrid = new Grid {Name = "MenuButtonGrid", ShowGridLines = true};
            var highscoresGrid = new DataGrid {Name = "HighscoresGrid"};
            rootGrid.Children.Add(highscoresGrid);
            rootGrid.Children.Add(menuButtonGrid);
            Grid.SetColumn(menuButtonGrid, 1);

            var panel = new StackPanel{ Focusable = true, VerticalAlignment = VerticalAlignment.Center, Name = "ButtonPanel"};
            menuButtonGrid.Children.Add(panel);

            var startbtn = new Button {Content = "Start"};
            var continueBtn = new Button {Content = "Continue" };

            panel.Children.Add(startbtn);
            panel.Children.Add(continueBtn);
        }
    }
}