using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace Logistics.Utility
{
    public interface ITickTimer
    {
        public DispatcherTimer Timer { get; set; }
        public List<string> EventsList { get; set; }
    }
}
