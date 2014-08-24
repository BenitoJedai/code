using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#System/services/monitoring/system/diagnosticts/Stopwatch.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Diagnostics/Stopwatch.cs

    [Script(Implements = typeof(global::System.Diagnostics.Stopwatch))]
    public class __Stopwatch
    {
        public bool IsRunning
        {
            get;
            set;
        }


        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224
        // startTimeStamp
        public DateTime InternalStart = DateTime.Now;
        public DateTime InternalStop = DateTime.Now;

        public void Start()
        {
            IsRunning = true;
            InternalStart = DateTime.Now;
        }

        public void Stop()
        {
            IsRunning = false;
            InternalStop = DateTime.Now;
        }


        public void Restart()
        {
            Stop();
            Start();
        }


        public TimeSpan Elapsed
        {
            get
            {
                if (IsRunning)
                    InternalStop = DateTime.Now;

                return InternalStop - InternalStart;
            }
        }

        public long ElapsedMilliseconds
        {
            get
            {

                return Convert.ToInt64(Elapsed.TotalMilliseconds);
            }
        }

        public const long TicksPerMillisecond = 10000;

        public long ElapsedTicks
        {
            get
            {
                // X:\jsc.svn\examples\javascript\appengine\AppEngineWhereOperator\AppEngineWhereOperator\ApplicationWebService.cs

                return ElapsedMilliseconds * TicksPerMillisecond;
            }
        }

        public override string ToString()
        {
            return this.Elapsed.ToString();
        }

        public static Stopwatch StartNew()
        {
            var x = new Stopwatch();

            x.Start();

            return x;
        }
    }
}
