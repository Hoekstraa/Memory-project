using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace Memory
{
    class State
    {
        MemoryGrid table;
        Player[] Players;

        public State(string[] players, int x, int y, string cardFolder) {
            this.table = new MemoryGrid(x, y, cardFolder);
            this.Players = new Player[players.Length];
            for (int i = 0; i < players.Length; i++) {
                this.Players[i] = new Player(players[i]);
            }
        }
    }

    /// <summary>
    /// Implementation of a memory card grid, the OOP way
    /// </summary>
    class MemoryGrid {
        private int sizeX;
        private int sizeY;
        MemoryCard[,] Cards;

        /// <summary>
        /// return the grid width
        /// </summary>
        /// <returns>integer value of sizeX</returns>
        public int getSizeX() {
            return this.sizeX;
        }
        /// <summary>
        /// returns grid height
        /// </summary>
        /// <returns>integer value of sizeY</returns>
        public int getSizeY() {
            return this.sizeY;
        }
        /// <summary>
        /// MemoryGrid constructor
        /// </summary>
        /// <param name="x">grid width</param>
        /// <param name="y">grid height</param>
        /// <param name="cardFolder">A folder containing a valid set of playing cards. 1 back.png and numbered cards from 1.png to 2147483647.png</param>
        /// <returns>Memory grid object</returns>
        public MemoryGrid(int x, int y, string cardFolder) {
            this.sizeX = x;
            this.sizeY = y;
            this.Cards = new MemoryCard[x, y];  // reserve memory for x*y cards
            MemoryCard[] cards = new MemoryCard[x * y];
            // lets first check if we have enough cards
            if (!Directory.Exists(cardFolder)) {
                throw new Exception("Folder with cards does not exist and/or is inaccesible: " + cardFolder);
            }
            // test folder for cards
            List<string> filePaths = new List<string>(Directory.GetFiles(cardFolder, "*.png", SearchOption.TopDirectoryOnly));

            if (!filePaths.Contains("Images/back.png"))
            {

                throw new Exception("back.png is missing from card deck: " + cardFolder + "back.png");
            }
            else {
                // remove back.jpg from List to make iteration easier
                filePaths.RemoveAt(filePaths.IndexOf("Images/back.png"));
            }
            // now lets check if we have enough unique pictures to create x*y/2 unique cards
            for (int i = 0; i < (x * y / 2); i++) {
                if (!File.Exists(cardFolder + (i + 1) + ".png"))
                {
                    throw new Exception("Could not satisfy demand for " + (x * y / 2) + " unique cards. Missing: " + cardFolder + (i + 1) + ".png");
                }
            }

            /*
             * Generate x*y cards with 1 duplicate of each.
             */
            for (int i = 0; i < x * y; i += 2) {
                cards[i] = new MemoryCard(i / 2, cardFolder + (i + 1) + ".png", cardFolder + "back.png");
                cards[i + 1] = new MemoryCard(i / 2, cardFolder + (i + 1) + ".png", cardFolder + "back.png");
            }
            // shuffle array thanks to ♥ IEnumerables ♥ and LINQ magic
            Random rnd = new Random();
            MemoryCard[] shuffled = cards.OrderBy(a => rnd.Next()).ToArray();

            /*
             * populate 2d array with our newly shuffled cards
             */
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    // treat array like a stack, grab last item from the array, use it and remove it from the array;
                    this.Cards[i, j] = shuffled.Last();
                    Array.Resize(ref shuffled, shuffled.Length - 1); // Slice last item from array

                }
            }
        }
    }
    /// <summary>
    /// Contains simple player information
    /// </summary>
    class Player {
        string Name;
        int score;
        public Player(string name) {
            this.Name = name;
        }
    }
    /// <summary>
    /// Contains card information, its matching id, the pictures and if it's flipped/disabled
    /// </summary>
    class MemoryCard {
        private int Id;
        public readonly string CardFront;
        public readonly string CardBack;
        public bool Flipped;
        public bool Disabled;
        /// <summary>
        /// MemoryCard constructor
        /// </summary>
        /// <param name="id">the identifier of the card, used for comparisons and such.</param>
        /// <param name="cardFront">path to the image used for the card</param>
        /// <param name="cardBack">path to the image used for the back of the card</param>
        public MemoryCard(int id, string cardFront, string cardBack) {
            this.Id = id;
            this.CardFront = cardFront;
            this.CardBack = cardBack;
            this.Flipped = false;
            this.Disabled = false;
        }
    }
    /// <summary>
    /// Manage state objects/ save/loading
    /// </summary>
    static class StateManagement {
        /// <summary>
        /// Save the state to an xml file
        /// </summary>
        /// <param name="state">State object to serialize</param>
        /// <param name="file">full path to the file where the state must be stored/param>
        static void SaveStateToFile(State state, string file) {
            XmlSerializer serializer = new XmlSerializer(typeof(State));
            string xml = "";
            using (StringWriter sw = new StringWriter()) {
                using (XmlWriter writer = XmlWriter.Create(sw)) {
                    serializer.Serialize(writer, state);
                    xml = sw.ToString();
                }
            }
            File.WriteAllText(file, xml);

        }
        /// <summary>
        /// Loads xml file into State object
        /// </summary>
        /// <param name="file">full path to saved state</param>
        /// <returns>State object containing state from file</returns>
        static State loadStateFromFile(string file) {
            State state;
            string xml = File.ReadAllText(file);
            XmlSerializer serializer = new XmlSerializer(typeof(State));
            using (TextReader reader = new StringReader(xml)) {
                state = (State)serializer.Deserialize(reader);
            }
                return state;
        }
    }

}
