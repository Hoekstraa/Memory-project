using System;
using System.Collections;

namespace Hashtables
{
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
            var matrix = NewEmpty(); // Make new matrix
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