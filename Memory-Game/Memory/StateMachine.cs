using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Memory
{
    public enum State
    {
        STOPPED,    // State machine is stopping or stopped.
        RUNNING,    // State machine is running.
        READY,      // State machine is ready for execution.
        FINISHED    // State machine has finished execution. (game end state reached)
    }

    class StateMachine
    {
        private State Mode;                                     // Can either be STOPPED or RUNNING, freezes/unfreezes the state machine
        private Hashtable Cards;                                // Hashtable containing all cards
        public event Action<object, ObserverArgs> ModeChange;   // triggered when started
        public event Action<object, ObserverArgs> Stopped;      // triggered when mode has changed.
        public event Action<object, ObserverArgs> CardMatch;    // triggered when two cards match
        public event Action<object, ObserverArgs> CardMisMatch; // triggered when two cards are mismatched

        public void SetMode(State newState)
        {
            this.Mode = newState;

            Action<object, ObserverArgs> handler = ModeChange;
            if (ModeChange != null)
            {
                ObserverArgs args = new ObserverArgs();
                args.Event = EventType.STATE_CHANGED;
                handler(this, args);
            }
        }
        public State getMode() {
            return this.Mode;
        }

        public void tick() {


            // Lets assume there is a match detected, notify all subscribers
            Action<object, ObserverArgs> handler = CardMatch;
            if (CardMatch != null)
            {
                ObserverArgs args = new ObserverArgs();
                args.Event = EventType.CARD_MATCH;
                handler(this, args);
            }

        }
    }

    class Logger : Observer {

        override public void HandleEvent(object sender, ObserverArgs args) {
            switch (args.Event) {
                case EventType.STATE_CHANGED:
                    Trace.WriteLine("State machine has changed. New state: "+ ((StateMachine)sender).getMode());
                    break;
                case EventType.CARD_MATCH:
                    Trace.WriteLine("I didn't get called because i didn't subscribe to the event :(((");
                    break;
            }
            
        }
    }
}
