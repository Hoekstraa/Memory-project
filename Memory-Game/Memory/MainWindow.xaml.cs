using System.Windows;
using System.Windows.Controls;
using System.Collections;
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
        public static Hashtable[,] gameBoard;
        public static Grid cardGrid;
        public MainWindow()
        {
            InitializeComponent();
            gameBoard = Matrix.Make(Card.Shuffled(Constant.AllUnique));
            Matrix.TraceBoard(gameBoard);
            var rootGrid = new Grid {Name = "RootGrid"};

            //var hoofdmenu = new Grid();
            //rootGrid.Children.Add(hoofdmenu);
            cardGrid = GameLogic.FillCardGrid(gameBoard, GameLogic.GenerateCardGrid());
            rootGrid.Children.Add(cardGrid);
            MWindow.Content = rootGrid;
        }

    }
}