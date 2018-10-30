using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            var menuRootGrid = new Grid {Name = "MenuRootGrid", ShowGridLines = true};
            GenerateMenuGrid(menuRootGrid);
            MWindow.Content = menuRootGrid; //Switch between menu and game screens

            // Generating a new state is this easy!!!!!!!!
            var state = new State(new[]{"jack", "jeff"}, Constant.Height, Constant.Width, @"Images/");
        }

        /// <summary>
        ///     Generates the grid the game is played in
        /// </summary>
        /// <param name="rootGrid">Grid to put playgrid into</param>
        private static void GeneratePlayGrid(Grid rootGrid)
        {
            CardGrid = GameLogic.FillCardGrid(GameBoard, GameLogic.GenerateCardGrid());

            var col2 = new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)};
            var col1 = new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)};

            rootGrid.ColumnDefinitions.Add(col2);
            rootGrid.ColumnDefinitions.Add(col1);
            rootGrid.Children.Add(CardGrid);

            var playingInfo = new StackPanel{ Name = "PlayingInfo", VerticalAlignment = VerticalAlignment.Center};
            rootGrid.Children.Add(playingInfo);
            Grid.SetColumn(playingInfo, 1);

            var margin = new System.Windows.Thickness(10);

            var player1Name = new TextBlock
            {
                Text = new Player("Player 1").Name,
                Foreground = new SolidColorBrush(Colors.Blue),
                TextAlignment = TextAlignment.Center,
                FontSize = 16
            };
            var player2Name = new TextBlock
            {
                Text = new Player("Player 2").Name,
                Foreground = new SolidColorBrush(Colors.Black),
                TextAlignment = TextAlignment.Center,
                FontSize = 16
            };

            var player1Score = new TextBlock
            {
                Text = new Player("Player 1").Score.ToString(),
                Foreground = new SolidColorBrush(Colors.Blue),
                TextAlignment = TextAlignment.Center,
                Margin = margin,
                FontSize = 16
            };
            var player2Score = new TextBlock
            {
                Text = new Player("Player 2").Score.ToString(),
                Foreground = new SolidColorBrush(Colors.Black),
                TextAlignment = TextAlignment.Center,
                Margin = margin,
                FontSize = 16
            };

            playingInfo.Children.Add(player1Name);
            playingInfo.Children.Add(player1Score);
            playingInfo.Children.Add(player2Name);
            playingInfo.Children.Add(player2Score);

            var opslaanKnop = new Button{Content = "Spel opslaan", Margin = margin, FontSize = 14};
            var resetKnop = new Button{Content = "Spel resetten", Margin = margin, FontSize = 14 };

            playingInfo.Children.Add(opslaanKnop);
            playingInfo.Children.Add(resetKnop);

        }

        /// <summary>
        ///     Generates the main menu
        /// </summary>
        /// <param name="rootGrid"></param>
        private void GenerateMenuGrid(Grid rootGrid)
        {
            var col2 = new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)}; // 2/3 of screen
            var col1 = new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}; // 1/3 of screen

            rootGrid.ColumnDefinitions.Add(col2);
            rootGrid.ColumnDefinitions.Add(col1);

            var menuButtonGrid = new Grid {Name = "MenuButtonGrid", ShowGridLines = true};

            rootGrid.Children.Add(menuButtonGrid);
            Grid.SetColumn(menuButtonGrid, 1);

            var panel = new StackPanel{ Focusable = true, VerticalAlignment = VerticalAlignment.Center, Name = "ButtonPanel"};
            menuButtonGrid.Children.Add(panel);

            #region define buttons

            var startbtn = new Button { Content = "Start" };
            startbtn.Click += PreStartbtn_Click;

            var continueBtn = new Button { Content = "Continue" };

            var highscorebtn = new Button { Content = "Highscore" };
            highscorebtn.Click += Highscorebtn_Click;

            var exitBtn = new Button { Content = "Exit" };
            exitBtn.Click += Exitbtn_Click;


            #endregion

            panel.Children.Add(startbtn);
            panel.Children.Add(continueBtn);
            panel.Children.Add(highscorebtn);
            panel.Children.Add(exitBtn);
        }

        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            var gameRootGrid = new Grid { Name = "GameRootGrid", ShowGridLines = true };
            GeneratePlayGrid(gameRootGrid);
            MWindow.Content = gameRootGrid;
        }

        /// <summary>
        /// Generates and displays gameroot grid when startbutton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreStartbtn_Click(object sender, RoutedEventArgs e)
        {
            var optionGrid = new Grid { Name = "HighscoresGrid" };
            var menuRootGrid = new Grid { Name = "MenuRootGrid", ShowGridLines = true };
            GenerateMenuGrid(menuRootGrid);
            menuRootGrid.Children.Add(optionGrid);

            var optionPanel = new StackPanel{VerticalAlignment = VerticalAlignment.Center};
            var name1Block = new TextBlock{Text = "Speler 1 naam:"};
            optionPanel.Children.Add(name1Block);
            var name1Box = new TextBox {Name = "Name1box"};
            optionPanel.Children.Add(name1Box);

            var name2Block = new TextBlock { Text = "Speler 2 naam:" };
            optionPanel.Children.Add(name2Block);
            var name2Box = new TextBox { Name = "Name2box" };
            optionPanel.Children.Add(name2Box);

            var actualstartbtn = new Button{Content = "Start game"};
            actualstartbtn.Click += Startbtn_Click;
            optionPanel.Children.Add(actualstartbtn);

            menuRootGrid.Children.Add(optionPanel);
            MWindow.Content = menuRootGrid;
        }

        private void Highscorebtn_Click(object sender, RoutedEventArgs e)
        {
            var highscoresGrid = new DataGrid { Name = "HighscoresGrid" };
            //load data from disk to grid
            var menuRootGrid = new Grid { Name = "MenuRootGrid", ShowGridLines = true };
            GenerateMenuGrid(menuRootGrid);
            menuRootGrid.Children.Add(highscoresGrid);
            MWindow.Content = menuRootGrid;
        }

        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Exitbtn_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown(0);
    }
}