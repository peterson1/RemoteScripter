using CommonTools.Lib11.FileSystemTools;
using System;

namespace CommonTools.Lib45.FileSystemTools
{
    public class ThrottledFileWatcher1 : FileChangeWatcher1, IThrottledFileWatcher
    {
        private DateTime _lastRaise;


        public uint IntervalMS { get; set; } = 1000;


        protected override void RaiseFileChanged()
        {
            var now = DateTime.Now;
            if (IsTooSoon(now)) return;

            base.RaiseFileChanged();
            _lastRaise = now;
        }


        private bool IsTooSoon(DateTime now)
        {
            var elapsd = (now - _lastRaise).TotalMilliseconds;
            return elapsd < IntervalMS;
        }
    }
}
