using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// why not in .Extensions
namespace System.Diagnostics
{
    // when will there be a name clash? where shall this type live?
    public static class StopwatchExtensions
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversionsForStopwatch.cs

        public static Stopwatch CreateStopwatchAtElapsed(TimeSpan t)
        {
            // http://msdn.microsoft.com/en-us/magazine/cc163996.aspx
            // http://msdn.microsoft.com/en-us/library/windows/desktop/ms644905(v=vs.85).aspx

            var Frequency = Stopwatch.Frequency;

            //Console.WriteLine("enter CLR_StringConversionsForStopwatchExtensions.CreateStopwatchAtElapsed. this shall only happen in clr. " + new { Debugger.IsAttached });

            //            Stopwatch.GetTimestamp()	0x0000001fa3aa613e	long
            //Stopwatch.IsHighResolution	true	bool


            //var nn = new Stopwatch();
            //nn.Start();
            var n = new Stopwatch();




            // we will simulate n.Start();
            var startTimeStamp = n.GetType().GetField("startTimeStamp", Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance);

            var ticks = Stopwatch.GetTimestamp();

            var elapsed = t.TotalMilliseconds * Stopwatch.Frequency / 1000;

            var start = ticks - (long)elapsed;

            //System.Threading.Thread.Sleep(t);

            //var stop = Stopwatch.GetTimestamp();

            //var elapsed = stop - ticks;
            // 		elapsed * 1000 / Frequency	= 211	long
            //ticks -= t.Ticks;

            startTimeStamp.SetValue(
                n, start
            );

            n.GetType().GetField("isRunning", Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance).SetValue(
                n, true
            );

            //Console.WriteLine("CLR_StringConversionsForStopwatchExtensions.CreateStopwatchAtElapsed  " + new { n.ElapsedMilliseconds, n.IsRunning });

            return n;
        }
    }
}
