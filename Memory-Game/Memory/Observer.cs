using System;

namespace Memory
{
    public enum EventType {
        StateChanged,
        CardMatch,
        CardMismatch,
    }
    public abstract class Observer
    {
        public abstract void HandleEvent(object sender,  ObserverArgs args);
    }

    public class ObserverArgs : EventArgs
    {
        public EventType Event;
    }
}
