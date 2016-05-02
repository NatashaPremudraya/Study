using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    public class Timer
    {
        private Stopwatch stopwatch = new Stopwatch();
        public long ElapsedMilliseconds => stopwatch.ElapsedMilliseconds;

        public TimerToken Start()
        {
            stopwatch.Start();
            return new TimerToken(this);
        }

        private void Stop()
        {
            stopwatch.Stop();
        }

        public TimerToken Continue()
        {
            stopwatch.Start();
            return new TimerToken(this);
        }

        public class TimerToken : IDisposable
        {
            private Timer timer;
            public TimerToken(Timer timer)
            {
                this.timer = timer;
            }

            public void Dispose()
            {
                timer.Stop();
            }
        }
    }
}
