using System;
using System.Collections;

namespace Hashtables
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Hashtable[,] game_board = Matrix.Make(Card.allUnique);
            
            Card.PrintAll();

            Console.ReadKey();
        }
    }

    partial class Card
    {
        public static readonly Hashtable[] allUnique = new Hashtable[]
        {
            Card.New(1), Card.New(1), Card.New(2), Card.New(2), Card.New(3), Card.New(3),
            Card.New(4), Card.New(4), Card.New(5), Card.New(5), Card.New(6), Card.New(6),
            Card.New(7), Card.New(7), Card.New(8), Card.New(8)
        };
        public static void PrintAll()
        {
            foreach (Hashtable table in Card.allUnique)
            {
                foreach (DictionaryEntry entry in table)
                {
                    Console.WriteLine($"{entry.Key} : {entry.Value}");
                }
            };
        }

        public static Hashtable New(int number)
        {
            return new Hashtable
            {
                { "Flipped", false },
                { "Image", new Uri($"file://{number}.png") },
                { "Number", number }
            };
        }
    }

    partial class Matrix
    {
        public static Hashtable[,] NewEmpty()
        {
            return new Hashtable[,] { { }, { }, { }, { } };
        }

        public static Hashtable[,] Make(Hashtable[] allCards)
        {
            var empty_array = Matrix.NewEmpty(); // Make new matrix
            // Shuffle allCards
            // For each card in shuffled array
            //
            var matrix = Matrix.NewEmpty();
            return matrix;
        }
    }
}