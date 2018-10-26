using System.Collections;
using System.Diagnostics;

namespace Memory
{
    /// <summary>
    ///     Holds code regarding matrices
    /// </summary>
    internal class Matrix
    {
        /// <summary>
        ///     Make a new 2d hashtable array with correct Height/Width
        /// </summary>
        /// <returns>Empty matrix</returns>
        public static Hashtable[,] NewEmpty
            => new Hashtable[Constant.Height, Constant.Width];

        /// <summary>
        ///     Fill array with all cards
        /// </summary>
        /// <param name="allCards"></param>
        /// <returns>matrix filled with cards</returns>
        public static Hashtable[,] Make(Hashtable[] allCards)
        {
            var matrix = NewEmpty; // Make new matrix
            var counter = 0;
            for (var i = 0; i < Constant.Width; i++)
            for (var j = 0; j < Constant.Height; j++)
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
        ///     Print matrix to console, in a square. Kept for debugging purposes
        /// </summary>
        /// <param name="matrix"></param>
        public static void PrintToConsole(Hashtable[,] matrix)
        {
            for (var i = 0; i < Constant.Width; i++)
            for (var j = 0; j < Constant.Height; j++)
            {
                Trace.Write(matrix[i, j]["Number"]);
                    if (j == 3) Trace.Write("/n");
            }
        }
        /// <summary>
        ///     Print matrix to 'output', in a square
        /// </summary>
        /// <param name="matrix"></param>
        public static void TraceBoard(Hashtable[,] matrix)
        {
            for (var i = 0; i < Constant.Width; i++)
            for (var j = 0; j < Constant.Height; j++)
            {
                Trace.Write($"{matrix[i, j]["Number"]} ");
                if (j == 3) Trace.WriteLine("");
            }
        }
    }
}