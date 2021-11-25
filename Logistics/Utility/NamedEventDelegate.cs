using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Utility
{
    public class NamedEventDelegate
    {
        public EventHandler Event;
        public string EventName { get; set; }
        public EDelegateType Type { get; set; }
    }
}
