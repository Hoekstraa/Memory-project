using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Memory
{
    public class GameLogic
    {
        /// <summary>
        ///     Revolves card in both the gameboard and the UI grid
        /// </summary>
        /// <param name="row">row in gameboard</param>
        /// <param name="column">column in gameboard</param>
        /// <param name="gameBoard">gameboard to revolve card in</param>
        /// <param name="cardGrid">UI element to revolve card in</param>
        public static void RevolveCard(int row, int column, Hashtable[,] gameBoard, Grid cardGrid)
        {
            var btn = new Button();
            Image x;
            if ((bool)gameBoard[row, column]["Flipped"])
            {
                gameBoard[row, column]["Flipped"] = false;
                x = CreateImage(gameBoard[row, column], "Back");
                btn.Content = x;
            }
            else
            {
                gameBoard[row, column]["Flipped"] = true;
                x = CreateImage(gameBoard[row, column], "Front");
                btn.Content = x;
            }

            btn.Click += Btn_Click;
            cardGrid.Children.Add(btn);
            cardGrid.Children.Remove(GetGridElement(cardGrid, row, column));
            Grid.SetRow(btn, row);
            Grid.SetColumn(btn, column);
        }

        /// <summary>
        ///     Retrieves element where row = r and column = c
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="r">row number</param>
        /// <param name="c">column number</param>
        /// <returns></returns>
        private static UIElement GetGridElement(Grid g, int r, int c)
        {
            for (var i = 0; i < g.Children.Count; i++)
            {
                var e = g.Children[i];
                if (Grid.GetRow(e) == r && Grid.GetColumn(e) == c)
                    return e;
            }

            return null;
        }

        /// <summary>
        ///     Takes card matrix maps its images onto the GUI
        /// </summary>
        /// <param name="matrix">Representation of gameboard in 2d array of hashtables</param>
        /// <param name="cardGrid">Grid to be filled with Images</param>
        /// <returns>New modified grid</returns>
        public static Grid FillCardGrid(Hashtable[,] matrix, Grid cardGrid)
        {
            var newGrid = cardGrid;
            for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                {
                    var x = CreateImage(matrix[i, j], "Back");
                    var btn = new Button();
                    btn.Content = x;
                    btn.Click += Btn_Click;
                    newGrid.Children.Add(btn);
                    Grid.SetRow(btn, j);
                    Grid.SetColumn(btn, i);
                }

            return newGrid;
        }

        /// <summary>
        /// Gets row and column of clicked button and returns it to trace output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Btn_Click(object sender, RoutedEventArgs e) //, Grid cardGrid, Hashtable[,] gameBoard)
        {
            var row = Grid.GetRow((UIElement)e.OriginalSource);
            var column = Grid.GetColumn((UIElement)e.OriginalSource);
            Trace.WriteLine($"{row}, {column}");
            PlayerLogic(sender, column, row);
        }

        /// <summary>
        ///     Add x amount of columns to a grid, MUTATES the grid
        /// </summary>
        /// <param name="grid">Grid to add columns to</param>
        /// <param name="amount">Amount of columns to be added</param>
        private static void AddColumns(Grid grid, int amount = 1)
        {
            for (var i = 0; i < amount; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        /// <summary>
        ///     Add x amount of rows to a grid, MUTATES the grid
        /// </summary>
        /// <param name="grid">Grid to add rows to</param>
        /// <param name="amount">Amount of rows to be added</param>
        private static void AddRows(Grid grid, int amount = 1)
        {
            for (var i = 0; i < amount; i++)
                grid.RowDefinitions.Add(new RowDefinition());
        }

        /// <summary>
        ///     Generates gameplay screen dynamically
        /// </summary>
        /// <returns>New grid</returns>
        public static Grid GenerateCardGrid()
        {
            var cardGrid = new Grid { Name = "CardGrid", ShowGridLines = true };

            AddColumns(cardGrid, 4);
            AddRows(cardGrid, 4);

            return cardGrid;
        }

        /// <summary>
        ///     Returns Image based on the cardNumber
        /// </summary>
        /// <param name="card"></param>
        /// <param name="side">either back or front</param>
        /// <returns>new image</returns>
        private static Image CreateImage(IDictionary card, string side)
        {
            var simpleImage = new Image { Width = 200, Margin = new Thickness(5) };

            var bi = new BitmapImage();

            bi.BeginInit();

            bi.UriSource = (Uri)card[side];

            bi.EndInit();

            simpleImage.Source = bi;
            return simpleImage;
        }


        /// <summary>
        ///     Memory logic executed upon clicking a card
        /// </summary>
        /// <param name="sender">sender button object, currently unused</param>
        /// <param name="x">x axis of the card clicked</param>
        /// <param name="y">y axis of the card clicked</param>
        /// <returns>void</returns>
        private static void PlayerLogic(object sender, int x, int y) {
            GameLogic.RevolveCard(y, x, MainWindow.gameBoard, MainWindow.cardGrid);
            // int[0] = row
            // int[1] = column

            //Abstraction layer for certain code to make the game logic more readable
            //List<int[]> with 2 entries contains an int array with sizeof() == int*2 of information regarding the x & y axis of a flipped card
            List<int[]> GetFlippedCards() {
                var newFlippedCards = new List<int[]>();
                for (var i = 0; i < Constant.Height; i++) {
                    for (var j = 0; j < Constant.Width; j++) {
                        if (!(bool) MainWindow.gameBoard[i, j]["Flipped"]) continue;
                        var flippedCard = new int[2];
                        flippedCard[0] = i;
                        flippedCard[1] = j;
                        newFlippedCards.Add(flippedCard);
                    }
                }
                return newFlippedCards;
            }

            List<int[]> flippedCards = GetFlippedCards();

            //Abstraction for certain comparisons to make the actual game logic more readable
            //true if 2 cards are the same
            bool CompareFlippedCards()
            {
                var card1 = (int) MainWindow.gameBoard[flippedCards[0][0], flippedCards[0][1]]["Number"];
                var card2 = (int) MainWindow.gameBoard[flippedCards[1][0], flippedCards[1][1]]["Number"];
                return card1 == card2;
            }


            if (flippedCards.Count != 2) return; // if 2 cards flipped, lets see if they match
            Trace.WriteLine("2 cards flipped!!");
            if (CompareFlippedCards())
            {
                Trace.WriteLine("Match!");
                // TODO: Code that executes when 2 cards match
                // • Ignore these cards
                // • Add score
            }
            else {
                Trace.WriteLine("no match!");
                // TODO: Code that executes when 2 cards do not match
                // • Flip cards back & end turn for current player
            }
        }
    }
}