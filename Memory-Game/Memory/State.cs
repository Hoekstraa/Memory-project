using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Memory
{
    public class State
    {
        public List<Player> Players;
        public int CurrentPlayer;
        public MemoryGrid Table;

        public State(string[] players, int x, int y, string cardFolder)
        {
            Players = new List<Player>();
            Table = new MemoryGrid(x, y, cardFolder);
            for (var i = 0; i < players.Count(); i++)
            {
                Players.Add(new Player(players[i]));
            }
            CurrentPlayer = 0;
           
        }
    }

    /// <summary>
    ///     Implementation of a memory card grid, the OOP way
    /// </summary>
    public class MemoryGrid
    {
        public MemoryCard[,] _cards;
        public readonly Grid _skeleton;
        private int _sizeX;
        private int _sizeY;
        /// <summary>
        ///     MemoryGrid constructor
        /// </summary>
        /// <param name="x">grid width</param>
        /// <param name="y">grid height</param>
        /// <param name="cardFolder">
        ///     A folder containing a valid set of playing cards. 1 back.png and numbered cards from 1.png to
        ///     2147483647.png
        /// </param>
        /// <returns>Memory grid object</returns>
        public MemoryGrid(int x, int y, string cardFolder)
        {
            _sizeX = x;
            _sizeY = y;
            _cards = new MemoryCard[x, y]; // reserve memory for x*y cards

            var cards = new MemoryCard[x * y];

            // lets check if our grid has an *even* amount of cards
            if (Convert.ToBoolean(x * y % 2))
            {
                throw new Exception("Attempted to generate grid with an uneven amount of cards... ");
            }


            // lets check if we have enough cards
            if (!Directory.Exists(cardFolder))
                throw new Exception("Folder with cards does not exist and/or is inaccesible: " + cardFolder);

            // test folder for cards
            var filePaths = new List<string>(Directory.GetFiles(cardFolder, "*.png", SearchOption.TopDirectoryOnly));

            if (!filePaths.Contains("Images/back.png"))
                throw new Exception("back.png is missing from card deck: " + cardFolder + "back.png");

            filePaths.RemoveAt(filePaths.IndexOf("Images/back.png"));

            // now lets check if we have enough unique pictures to create x*y/2 unique cards
            for (var i = 0; i < x * y / 2; i++)
                if (!File.Exists(cardFolder + (i + 1) + ".png"))
                    throw new Exception("Could not satisfy demand for " + x * y / 2 + " unique cards. Missing: " +
                                        cardFolder + (i + 1) + ".png");

            /*
             * Generate x*y cards with 1 duplicate of each.
             */
            for (var i = 0; i < x * y; i += 2)
            {
                cards[i] = new MemoryCard(i / 2, cardFolder + (i / 2 + 1) + ".png", cardFolder + "back.png");
                cards[i + 1] = new MemoryCard(i / 2, cardFolder + (i / 2 + 1) + ".png", cardFolder + "back.png");
            }

            // shuffle array thanks to ♥ IEnumerables ♥ and LINQ magic
            var rnd = new Random();
            var shuffled = cards.OrderBy(a => rnd.Next()).ToArray();

            /*
             * populate 2d array with our newly shuffled cards
             */
            for (var i = 0; i < x; i++)
                for (var j = 0; j < y; j++)
                {
                    // treat array like a stack, grab last item from the array, use it and remove it from the array;
                    _cards[i, j] = shuffled.Last();
                    Array.Resize(ref shuffled, shuffled.Length - 1); // Slice last item from array
                }

            // geneate empty grid for our game
        }
        /// <summary>
        ///     Generates grid to accomodate all our cards.
        /// </summary>
        /// <returns>Grid object</returns>

        /// <summary>
        ///     return the grid width
        /// </summary>
        /// <returns>integer value of sizeX</returns>
        public int GetSizeX()
        {
            return _sizeX;
        }

        /// <summary>
        ///     returns grid height
        /// </summary>
        /// <returns>integer value of sizeY</returns>
        public int GetSizeY()
        {
            return _sizeY;
        }
        /// <summary>
        ///     Logica upon button click.
        /// </summary>
        /// <returns>integer value of sizeY</returns>
        public List<cardVector> GetFlippedCards()
        {
            List<cardVector> cards = new List<cardVector>();
            for (int i = 0; i < this.GetSizeX(); i++)
            {
                for (int j = 0; j < this.GetSizeY(); j++)
                {
                    if (_cards[i, j].Flipped && !_cards[i,j].Disabled)
                    {
                        cards.Add(new cardVector(j, i));
                    }

                }
            }
            return cards;
        }
    }

        /// <summary>
        ///     Contains simple player information
        /// </summary>
    public class Player
    {
        public string Name { get; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }


    /// <summary>
    ///     Contains card information, its matching id, the pictures and if it's flipped/disabled
    /// </summary>
    public class MemoryCard
    {
        public readonly string CardBack;
        public readonly string CardFront;
        public int Id;
        public bool Disabled;
        public bool Flipped;

        /// <summary>
        ///     MemoryCard constructor
        /// </summary>
        /// <param name="id">the identifier of the card, used for comparisons and such.</param>
        /// <param name="cardFront">path to the image used for the card</param>
        /// <param name="cardBack">path to the image used for the back of the card</param>
        public MemoryCard(int id, string cardFront, string cardBack)
        {
            Id = id;
            CardFront = cardFront;
            CardBack = cardBack;
            Flipped = false;
            Disabled = false;
        }
    }

    /// <summary>
    ///     Manage state objects/ save/loading
    /// </summary>
    public static class StateManagement
    {
        /// <summary>
        ///     Save the state to an xml file
        /// </summary>
        /// <param name="state">State object to serialize</param>
        /// <param name="file">full path to the file where the state must be stored</param>
        private static void SaveStateToFile(State state, string file)
        {
            var serializer = new XmlSerializer(typeof(State));
            var xml = "";
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, state);
                    xml = sw.ToString();
                }
            }

            File.WriteAllText(file, xml);
        }

        /// <summary>
        ///     Loads xml file into State object
        /// </summary>
        /// <param name="file">full path to saved state</param>
        /// <returns>State object containing state from file</returns>
        private static State LoadStateFromFile(string file)
        {
            State state;
            var xml = File.ReadAllText(file);
            var serializer = new XmlSerializer(typeof(State));
            using (TextReader reader = new StringReader(xml))
            {
                state = (State) serializer.Deserialize(reader);
            }

            return state;
        }

        /// <summary>
        ///     Loads xml file into State object
        /// </summary>
        /// <param name="state">state object to manipulate</param>
        private static void NextPlayer(State state) {
            if (state.Players.Count > (state.CurrentPlayer + 1))
            {
                state.CurrentPlayer++;
            }
            else {
                state.CurrentPlayer = 0;
            }
        }
        /// <summary>
        ///     Generates grid based on the current state of the game, filling the skeleton object.
        /// </summary>
        /// <param name="state">state object to manipulate</param>
        public static Grid GenerateGrid(State state) {
            Grid GridGen()
            {
                Grid gameGrid = new Grid();
                //gameGrid.ShowGridLines = true;

                gameGrid.MaxHeight = Int32.MaxValue;
                gameGrid.MaxWidth = Int32.MaxValue;
                ColumnDefinition[] cols = new ColumnDefinition[state.Table.GetSizeX()];
                RowDefinition[] rows = new RowDefinition[state.Table.GetSizeY()];
                for (int i = 0; i < state.Table.GetSizeX(); i++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(1, GridUnitType.Star);
                    gameGrid.ColumnDefinitions.Add(col);
                }
                for (int i = 0; i < state.Table.GetSizeY(); i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(1, GridUnitType.Star);
                    gameGrid.RowDefinitions.Add(row);
                }
                return gameGrid;
            }

            Image BuildCard(MemoryCard card) {
                // Create deep copy of _skeleton

                Image newCard = new Image();
                newCard.MaxHeight = Int32.MaxValue;
                newCard.MaxWidth = Int32.MaxValue;
                newCard.MouseLeftButtonUp += MouseButtonEventHandler;
                // lets see if the card is flipped or not to decide which image to paint.
                if (card.Flipped)
                {
                    newCard.Source = new BitmapImage(new Uri(card.CardFront, UriKind.Relative));
                } else {
                    newCard.Source = new BitmapImage(new Uri(card.CardBack, UriKind.Relative));
                    newCard.StretchDirection = StretchDirection.Both;
                    newCard.Stretch = System.Windows.Media.Stretch.UniformToFill;
                }
                return newCard;
            }
            Grid grid = GridGen();
            for (int i = 0; i < state.Table.GetSizeX(); i++) {
                for (int j = 0; j < state.Table.GetSizeY(); j++) {
                    Image card = BuildCard(state.Table._cards[i, j]);
                    card.Tag = new cardVector(j,i); // add coordinates meta data;

                    Grid.SetColumn(card, i);
                    Grid.SetRow(card, j);
                    grid.Children.Add(card);
                    Border border = new Border();
                    border.BorderThickness = new Thickness()
                    {
                        Bottom = 5,
                        Left = 5,
                        Right = 5,
                        Top = 5
                    };
                    border.BorderBrush = new SolidColorBrush(Colors.White);
                    Grid.SetRow(border, j);
                    Grid.SetColumn(border, i);
                    grid.Children.Add(border);
                }
            }

            return grid;
        }
        /// <summary>
        ///     Logica upon button click.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="System.Windows.Input.MouseButtonEventArgs">information about button click</param>
        public async static void MouseButtonEventHandler(Object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            State state = MainWindow.state;
            cardVector vector = (cardVector)(sender as Image).Tag;

            // fail safe to prevent multiple cards from being clicked;
            List<cardVector> raceCheck = state.Table.GetFlippedCards();
            if (raceCheck.Count == 2)
            {
                return;
            }

            Trace.WriteLine("Clicked card X:" + vector.X + " Y: " + vector.Y);
            /// flip card
            state.Table._cards[vector.Y, vector.X].Flipped = true;
            // redraw user interface
            MainWindow.control.Content = StateManagement.GenerateGrid(state);
            await logicDelay(); // wait a while before we evaluate cards to let users see their pick.

            // check flipped cards
            List<cardVector> flipped = state.Table.GetFlippedCards();
            if (flipped.Count == 2) {
                Trace.WriteLine("2 cards are flipped, lets check them!");
                if (state.Table._cards[flipped[0].Y, flipped[0].X].Id == state.Table._cards[flipped[1].Y, flipped[1].X].Id)
                {
                    Trace.WriteLine("cards match!");
                    state.Table._cards[flipped[0].Y, flipped[0].X].Disabled = true;
                    state.Table._cards[flipped[1].Y, flipped[1].X].Disabled = true;
                    state.Players[state.CurrentPlayer].Score++;
                }
                else {
                    Trace.WriteLine("Cards mismatch :(, change current player");
                    StateManagement.NextPlayer(state);
                    state.Table._cards[flipped[0].Y, flipped[0].X].Flipped = false;
                    state.Table._cards[flipped[1].Y, flipped[1].X].Flipped = false;
                }
                for (int i = 0; i < state.Players.Count; i++)
                {
                    Trace.WriteLine("Player: " + state.Players[i].Name + " Score:" + state.Players[i].Score);
                }
                Trace.WriteLine("Current player:" + state.Players[state.CurrentPlayer].Name);
            }


            MainWindow.control.Content = StateManagement.GenerateGrid(state);

            async Task logicDelay()
            {
                await Task.Delay(1000);
            }
        }
    }
    /// <summary>
    ///     Implementation of Vector2
    /// </summary>
    public class cardVector {
        public int X;
        public int Y;
        /// <summary>
        ///     Logica upon button click.
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        public cardVector(int x, int y) {
            this.X = x;
            this.Y = y;
        }
    }
}