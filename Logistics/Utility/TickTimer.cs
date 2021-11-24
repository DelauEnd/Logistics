using Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace Logistics.Utility
{
    public class TickTimer : ITickTimer
    {
        public DispatcherTimer Timer { get; set; }
        public List<string> EventsList { get; set; }

        public TickTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            Timer.Start();

            EventsList = new List<string>();
        }
    }
}
