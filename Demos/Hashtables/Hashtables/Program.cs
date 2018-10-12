using System;

namespace Hashtables
{
    /// <summary>
    /// Start of the program, makes calls to functions to:
    /// make a new shuffled matrix * 6 and print them out 1 by 1.
    /// </summary>
    internal class Program
    {
        private static void Main()
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
}