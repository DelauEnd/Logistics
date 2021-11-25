using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Logistics.Utility
{
    public class TickTimer : ITickTimer
    {
        public DispatcherTimer Timer { get; set; }
        private Dictionary<string, EDelegateType> EventsList { get; set; }

        public TickTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            Timer.Start();

            EventsList = new Dictionary<string, EDelegateType>();
        }

        public void AddEventIfNotExist(NamedEventDelegate elem)
        {
            if (IsContextRequest(elem) && HasContextRequests())
                return;

            if (!IventAlredyAdded(elem))
                AddEvent(elem);
        }

        private static bool IsContextRequest(NamedEventDelegate elem)
        {
            return elem.Type == EDelegateType.CONTEXT_REQUEST;
        }

        private bool HasContextRequests()
        {
            return EventsList.Values.Any(value => value == EDelegateType.CONTEXT_REQUEST);
        }

        private void AddEvent(NamedEventDelegate elem)
        {
            Timer.Tick += elem.Event;
            EventsList.Add(elem.EventName, elem.Type);
        }

        public void DeleteEventIfExist(NamedEventDelegate elem)
        {
            if (IventAlredyAdded(elem))
                DeleteEvent(elem);
        }

        private void DeleteEvent(NamedEventDelegate elem)
        {
            Timer.Tick -= elem.Event;
            EventsList.Remove(elem.EventName);
        }

        private bool IventAlredyAdded(NamedEventDelegate elem)
        {
            return EventsList.ContainsKey(elem.EventName);
        }
    }
}
