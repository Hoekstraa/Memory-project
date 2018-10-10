using System;
using System.Collections;

namespace Hashtables
{
    /// <summary>
    /// Holds constants and readonly variables
    /// </summary>
    partial class Constants
    {
        public const int height = 4;
        public const int width = 4;
        
        public static readonly Hashtable[] allUnique = new Hashtable[]
        {
            Card.New(1), Card.New(1), Card.New(2), Card.New(2), Card.New(3), Card.New(3),
            Card.New(4), Card.New(4), Card.New(5), Card.New(5), Card.New(6), Card.New(6),
            Card.New(7), Card.New(7), Card.New(8), Card.New(8)
        };
    }

    partial class Program
    {
        private static void Main(string[] args)
        {
            Hashtable[,] game_board = Matrix.Make(Card.Shuffled(Constants.allUnique));
            Matrix.PrintToConsole(game_board);

            for (int i = 0; i < 6; i++)
            {
                Console.Write("\n");
                game_board = Matrix.Make(Card.Shuffled(Constants.allUnique));
                Matrix.PrintToConsole(game_board);
            }


            Console.ReadKey();
        }
    }

    /// <summary>
    /// Holds code related to cards
    /// </summary>
    partial class Card
    {
        /// <summary>
        /// Print all Cards in Constants.allUnique
        /// </summary>
        public static void PrintAll()
        {
            foreach (Hashtable table in Constants.allUnique)
            {
                foreach (DictionaryEntry entry in table)
                {
                    Console.WriteLine($"{entry.Key} : {entry.Value}");
                }
            };
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

        public static Hashtable[] Shuffled(Hashtable[] allCards)
        {
            var arr = allCards;
            var rnd = new Random();
            for (int i = 0; i < arr.Length - 1; i++)
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
    partial class Matrix
    {
        /// <summary>
        /// Make a new 2d hashtable array with correct height/width
        /// </summary>
        /// <returns>Empty matrix</returns>
        public static Hashtable[,] NewEmpty()
        {
            return new Hashtable[Constants.height, Constants.width];
        }

        /// <summary>
        /// Fill array with all cards
        /// </summary>
        /// <param name="all cards"></param>
        /// <returns>matrix filled with cards</returns>
        public static Hashtable[,] Make(Hashtable[] allCards)
        {
            var matrix = Matrix.NewEmpty(); // Make new matrix
            // TODO: Shuffle allCards
            int counter = 0;

            for (int i = 0; i < Constants.width; i++)
            {
                for (int j = 0; j < Constants.height; j++)
                {
                    //Logging
                    //Console.WriteLine($"{i}, {j}");
                    //Console.WriteLine($"{allCards[counter]["Number"]} \n");
                    matrix[i, j] = allCards[counter];
                    counter += 1;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Print matrix to console, in square
        /// </summary>
        /// <param name="matrix"></param>
        public static void PrintToConsole(Hashtable[,] matrix)
        {
            for (int i = 0; i < Constants.width; i++)
            {
                for (int j = 0; j < Constants.height; j++)
                {
                    //Logging
                    //Console.WriteLine($"{i}, {j}");

                    Console.Write($"{matrix[i, j]["Number"]} ");
                    if (j == 3)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }
    }
}