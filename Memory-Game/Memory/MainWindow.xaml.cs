using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Start of the program, makes calls to functions to:
        /// make a new shuffled matrix * 6 and print them out 1 by 1.
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
        /// Takes array and maps it onto the GUI
        /// </summary>
        /// <param name="matrix"></param>
        private void BoardToScreen(Hashtable[,] matrix)
        {
            //
        }

        /// <summary>
        /// Generates gameplay screen dynamically
        /// </summary>
        private void InitPlayScreen()
        {
            var cardGrid = new Grid {Name = "CardGrid"};
        }
    }


}
