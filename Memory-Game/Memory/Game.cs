using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Memory
{
    class Game : Observer
    {
        override public void HandleEvent(object sender, ObserverArgs args)
        {
            switch (args.Event) {
                case EventType.CARD_MATCH:
                    Trace.WriteLine("WOAH THERE COWBOY, we got 2 matching cards!!!!");

                    // now we do stuff with those two matching cards
                    ExecuteThisWhenCardsMatch();
                    break;
                case EventType.CARD_MISMATCH:
                    // etc
                    break;
            }
        }

        private void ExecuteThisWhenCardsMatch() {
            // TODO: write code
        }
    }
}
