using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtables
{
    partial class Program
    {
        private static readonly Hashtable[] allCards = new Hashtable[] 
        {
            NewCard(1), NewCard(2), NewCard(3), NewCard(4), NewCard(5), NewCard(6),
            NewCard(7), NewCard(8), NewCard(9), NewCard(10), NewCard(11), NewCard(12),
            NewCard(13), NewCard(14), NewCard(15), NewCard(16)
        };

        static void Main(string[] args)
        {
            Hashtable[,] game_board = MakeGameMatrix(Program.allCards);

            Console.WriteLine(allCards.ToString());
            PrintAllCards();

            Console.ReadKey();
        }

        static Hashtable NewCard(int number)
        {
            return new Hashtable
            {
                { "Flipped", false },
                { "Image", new Uri($"file://{number}.png") },
                { "Number", number }
            };
        }

        static Hashtable[,] NewEmptyMatrix()
        {
            return new Hashtable[,] { {},{},{},{} };
        }

        static Hashtable[,] MakeGameMatrix(Hashtable[] allCards)
        {
            var empty_array = NewEmptyMatrix(); // Make new matrix
            // Shuffle allCards
            // For each card in shuffled array
            //
            var matrix = NewEmptyMatrix();
            return matrix;
        }

        static void PrintAllCards()
        {
            foreach(Hashtable table in allCards)
            {
                foreach(DictionaryEntry entry in table)
                {
                    Console.WriteLine($"{entry.Key} : {entry.Value}");
                }
            };
        }
    }
}
