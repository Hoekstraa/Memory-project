using System;
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
        public MainWindow()
        {
            InitializeComponent();
            PopulateButtons();
        }

        public void PopulateButtons()
        {
            int xPos;
            int yPos;
            int boardSize = 500;

            Random ranNum = new Random();

            for (int i = 0; i <9; i++) //We want 9 buttons
            {
                Button foo = new Button();
                
                int sizeValue = boardSize / 3;

                foo.Width = sizeValue;
                foo.Height = sizeValue;
                foo.Name = "card_" + i;

                xPos = ranNum.Next(boardSize - sizeValue);
                yPos = ranNum.Next(boardSize - sizeValue);

                foo.HorizontalAlignment = HorizontalAlignment.Left;
                foo.VerticalAlignment = VerticalAlignment.Top;
                foo.Margin = new Thickness(xPos, yPos, 0, 0);

                foo.Click += new RoutedEventHandler(ButtonClick);
                cardRoot.Children.Add(foo);
            }
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            Button clicked = (Button) sender;
            MessageBox.Show("Button's name is: " + clicked.Name);
        }
    }
}
