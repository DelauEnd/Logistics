using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Logistics.Utility
{
    public class TickTimer : ITickTimer
    {
        public DispatcherTimer Timer { get; set; }

        public TickTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            Timer.Start();
        }  
    }
}
