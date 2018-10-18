using System.Windows;
using System.Windows.Controls;

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
            var cardGrid = GameLogic.FillCardGrid(gameBoard, GameLogic.GenerateCardGrid());
            rootGrid.Children.Add(cardGrid);
            MWindow.Content = rootGrid;

            GameLogic.RevolveCard(0, 3, gameBoard, cardGrid);
            GameLogic.RevolveCard(0, 3, gameBoard, cardGrid);
            GameLogic.RevolveCard(2, 1, gameBoard, cardGrid);
            GameLogic.RevolveCard(1, 2, gameBoard, cardGrid);
            GameLogic.RevolveCard(3, 0, gameBoard, cardGrid);
        }

    }
}