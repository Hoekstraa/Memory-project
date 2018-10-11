using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public enum EventType {
        STATE_CHANGED,
        CARD_MATCH,
        CARD_MISMATCH,
    }
    public abstract class Observer
    {
        abstract public void HandleEvent(object sender,  ObserverArgs args);
    }

    public class ObserverArgs : EventArgs
    {
        public EventType Event;
    }
}
