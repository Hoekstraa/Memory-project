using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace Memory
{
    internal class State
    {
        private readonly Player[] _players;
        private MemoryGrid _table;

        public State(IReadOnlyList<string> players, int x, int y, string cardFolder)
        {
            _table = new MemoryGrid(x, y, cardFolder);
            _players = new Player[players.Count];
            for (var i = 0; i < players.Count; i++)
                _players[i] = new Player(players[i]);
        }
    }

    /// <summary>
    ///     Implementation of a memory card grid, the OOP way
    /// </summary>
    internal class MemoryGrid
    {
        private readonly MemoryCard[,] _cards;
        private readonly int _sizeX;
        private readonly int _sizeY;

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

            // lets first check if we have enough cards
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
                cards[i] = new MemoryCard(i / 2, cardFolder + (i + 1) + ".png", cardFolder + "back.png");
                cards[i + 1] = new MemoryCard(i / 2, cardFolder + (i + 1) + ".png", cardFolder + "back.png");
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
        }

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
    }

    /// <summary>
    ///     Contains simple player information
    /// </summary>
    internal class Player
    {
        public string Name {get;}
        public int Score {get;}

        public Player(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    ///     Contains card information, its matching id, the pictures and if it's flipped/disabled
    /// </summary>
    internal class MemoryCard
    {
        public readonly string CardBack;
        public readonly string CardFront;
        private int _id;
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
            _id = id;
            CardFront = cardFront;
            CardBack = cardBack;
            Flipped = false;
            Disabled = false;
        }
    }

    /// <summary>
    ///     Manage state objects/ save/loading
    /// </summary>
    internal static class StateManagement
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
    }
}