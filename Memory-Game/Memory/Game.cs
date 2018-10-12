using System;
using System.Diagnostics;

namespace Memory
{
    internal class Game : Observer
    {
        public override void HandleEvent(object sender, ObserverArgs args)
        {
            switch (args.Event) {
                case EventType.CardMatch:
                    Trace.WriteLine("WOAH THERE COWBOY, we got 2 matching cards!!!!");

                    // now we do stuff with those two matching cards
                    ExecuteThisWhenCardsMatch();
                    break;
                case EventType.CardMismatch:
                    // etc
                    break;
                case EventType.StateChanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ExecuteThisWhenCardsMatch() {
            // TODO: write code
        }
    }
}
