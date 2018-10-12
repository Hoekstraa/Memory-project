using System.Windows;

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var machine = new StateMachine();
            var logger = new Logger();
            var game = new Game();

            machine.ModeChange += logger.HandleEvent;
            machine.CardMatch += game.HandleEvent;
            machine.SetMode(State.Running);

            // execute one tick to raise a sample event
            machine.Tick();

            InitializeComponent();
        }
    }
}
