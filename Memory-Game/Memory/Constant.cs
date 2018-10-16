using System.Collections;

namespace Memory
{
    /// <summary>
    ///     Holds constants and readonly variables
    /// </summary>
    internal class Constant
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
}