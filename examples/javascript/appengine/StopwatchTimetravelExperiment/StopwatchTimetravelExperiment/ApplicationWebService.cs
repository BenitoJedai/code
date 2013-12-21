using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StopwatchTimetravelExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public Task<TheWatchers> WebMethod2(TheWatchers e)
        {
            e.Watch1.Start();

            Thread.Sleep(1111);

            e.Watch1.Stop();



            return e.ToTaskResult();
        }

    }

    public class TheWatchers
    {
        public Stopwatch Watch1;
    }
}
