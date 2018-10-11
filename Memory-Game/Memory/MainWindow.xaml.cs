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
            StateMachine machine = new StateMachine();
            Logger logger = new Logger();
            Game game = new Game();

            machine.ModeChange += logger.HandleEvent;
            machine.CardMatch += game.HandleEvent;
            machine.SetMode(State.RUNNING);

            // execute one tick to raise a sample event
            machine.tick();

            InitializeComponent();
        }
    }
}
