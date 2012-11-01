using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    public static class TimerExtensions
    {
        public static Timer Restart(this Timer t)
        {
            t.Stop();
            t.Start();

            return t;
        }
    }
}
