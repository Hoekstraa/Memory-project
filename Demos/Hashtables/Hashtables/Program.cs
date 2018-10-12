using System;
using System.Collections;

namespace Hashtables
{
    /// <summary>
    /// Start of the program, makes calls to functions to:
    /// make a new shuffled matrix * 6 and print them out 1 by 1.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gameBoard = Matrix.Make(Card.Shuffled(Constants.AllUnique));
            Matrix.PrintToConsole(gameBoard);

            for (var i = 0; i < 6; i++)
            {
                Console.Write("\n");
                gameBoard = Matrix.Make(Card.Shuffled(Constants.AllUnique));
                Matrix.PrintToConsole(gameBoard);
            }

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Holds constants and readonly variables
    /// </summary>
    internal class Constants
    {
        public const int Height = 4;
        public const int Width = 4;

        public static readonly Hashtable[] AllUnique =
        {
            Card.New(1), Card.New(1), Card.New(2), Card.New(2),
            Card.New(3), Card.New(3), Card.New(4), Card.New(4),
            Card.New(5), Card.New(5), Card.New(6), Card.New(6),
            Card.New(7), Card.New(7), Card.New(8), Card.New(8)
        };
    }

    /// <summary>
    /// Holds code related to cards
    /// </summary>
    internal class Card
    {
        /// <summary>
        /// Print all Cards in Constants.AllUnique
        /// </summary>
        public static void PrintAll()
        {
            foreach (var table in Constants.AllUnique)
                foreach (DictionaryEntry entry in table)
                    Console.WriteLine($"{entry.Key} : {entry.Value}");
        }

        /// <summary>
        /// Make new card hashtable with all data needed
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Hashtable New(int number)
        {
            return new Hashtable
            {
                { "Flipped", false },
                { "Image", new Uri($"file://{number}.png") },
                { "Number", number }
            };
        }

        /// <summary>
        /// Gets all cards and shuffles them inside a new array
        /// </summary>
        /// <param name="allCards"></param>
        /// <returns>New shuffled hashtable array</returns>
        public static Hashtable[] Shuffled(Hashtable[] allCards)
        {
            var arr = allCards;
            var rnd = new Random();
            for (var i = 0; i < arr.Length - 1; i++)
            {
                var j = rnd.Next(i, arr.Length);
                var temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            return arr;
        }
    }

    /// <summary>
    /// Holds code regarding matrices
    /// </summary>
    internal class Matrix
    {
        /// <summary>
        /// Make a new 2d hashtable array with correct Height/Width
        /// </summary>
        /// <returns>Empty matrix</returns>
        public static Hashtable[,] NewEmpty()
        {
            return new Hashtable[Constants.Height, Constants.Width];
        }

        /// <summary>
        /// Fill array with all cards
        /// </summary>
        /// <param name="allCards"></param>
        /// <returns>matrix filled with cards</returns>
        public static Hashtable[,] Make(Hashtable[] allCards)
        {
            var matrix = Matrix.NewEmpty(); // Make new matrix
            // TODO: Shuffle allCards
            var counter = 0;

            for (var i = 0; i < Constants.Width; i++)
                for (var j = 0; j < Constants.Height; j++)
                {
                    //Logging
                    //Console.WriteLine($"{i}, {j}");
                    //Console.WriteLine($"{allCards[counter]["Number"]} \n");
                    matrix[i, j] = allCards[counter];
                    counter += 1;
                }

            return matrix;
        }

        /// <summary>
        /// Print matrix to console, in a square
        /// </summary>
        /// <param name="matrix"></param>
        public static void PrintToConsole(Hashtable[,] matrix)
        {
            for (var i = 0; i < Constants.Width; i++)
                for (var j = 0; j < Constants.Height; j++)
                {
                    Console.Write($"{matrix[i, j]["Number"]} ");

                    if (j == 3)
                        Console.Write("\n");
                }
        }
    }
}