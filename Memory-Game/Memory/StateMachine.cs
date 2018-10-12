using System;
using System.Collections;
using System.Diagnostics;


namespace Memory
{
    public enum State
    {
        Stopped,    // State machine is stopping or stopped.
        Running,    // State machine is running.
        Ready,      // State machine is ready for execution.
        Finished    // State machine has finished execution. (game end state reached)
    }

    internal class StateMachine
    {
        private State _mode;                                     // Can either be STOPPED or RUNNING, freezes/unfreezes the state machine
        private Hashtable _cards;                                // Hashtable containing all cards
        public event Action<object, ObserverArgs> ModeChange;   // triggered when started
        public event Action<object, ObserverArgs> Stopped;      // triggered when mode has changed.
        public event Action<object, ObserverArgs> CardMatch;    // triggered when two cards match
        public event Action<object, ObserverArgs> CardMisMatch; // triggered when two cards are mismatched

        public void SetMode(State newState)
        {
            this._mode = newState;

            var handler = ModeChange;
            if (ModeChange == null) return;
            var args = new ObserverArgs {Event = EventType.StateChanged};
            handler?.Invoke(this, args);
        }
        public State GetMode() {
            return this._mode;
        }

        public void Tick() {
            // Lets assume there is a match detected, notify all subscribers
            var handler = CardMatch;
            if (CardMatch == null) return;
            var args = new ObserverArgs {Event = EventType.CardMatch};
            handler?.Invoke(this, args);
        }
    }

    internal class Logger : Observer {

        public override void HandleEvent(object sender, ObserverArgs args) {
            switch (args.Event) {
                case EventType.StateChanged:
                    Trace.WriteLine("State machine has changed. New state: "+ ((StateMachine)sender).GetMode());
                    break;
                case EventType.CardMatch:
                    Trace.WriteLine("I didn't get called because i didn't subscribe to the event :(((");
                    break;
                case EventType.CardMismatch:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
