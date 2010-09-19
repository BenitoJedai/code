using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Threading;

namespace ScriptCoreLib.Extensions.Avalon
{
    public static class AvalonUltraTimerExtensions
    {
        public static void AtIntervalWithTimerAndCounter(this int interval, Action<DispatcherTimer, int> h)
        {
            var c = -1;

            interval.AtIntervalWithTimer(
                t =>
                {
                    c++;

                    h(t, c);
                }
            );
        }
    }
}
