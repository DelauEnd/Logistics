using System.Windows.Threading;

namespace Logistics.Utility
{
    public interface ITickTimer
    {
        public DispatcherTimer Timer { get; set; }

        void AddEventIfNotExist(NamedEventDelegate elem);
        void DeleteEventIfExist(NamedEventDelegate elem);
    }
}
