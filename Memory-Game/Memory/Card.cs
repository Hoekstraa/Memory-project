﻿using System;
using System.Collections;
using System.Linq;

namespace Memory
{
    /// <summary>
    ///     Holds code related to cards
    /// </summary>
    internal class Card
    {
        /// <summary>
        ///     Print all Cards in Constants.AllUnique
        /// </summary>
        public static void PrintAll()
        {
            foreach (var entry in Constant.AllUnique.SelectMany(table => table.Cast<DictionaryEntry>()))
                Console.WriteLine($"{entry.Key} : {entry.Value}");
        }

        /// <summary>
        ///     Make new card hashtable with all data needed
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Hashtable New(int number)
        {
            return new Hashtable
            {
                {"Flipped", false},
                {"Image", new Uri($"Images/{number}.png", UriKind.Relative)},
                {"Number", number}
            };
        }

        /// <summary>
        ///     Gets all cards and shuffles them inside a new array
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
}