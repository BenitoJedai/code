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
        // Could not load file or assembly 'ScriptCoreLib.Extensions, 
        // Could not load file or assembly 'System.Data.SQLite, 
        // can jsc security analyzer go one level deeper? atleast on [script] [merge] assemblies?
        public ApplicationWebService()
        {
            { var r = typeof(global::System.Data.SQLite.SQLiteCommand); }
            { var r = typeof(global::ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda); }
        }


        public Stopwatch Sessionwatch = Stopwatch.StartNew();
        public Stopwatch SessionwatchReflection;

        //public XElement title;
        public XElement head;

        // autobind to HTML?
        public XElement output = new XElement("ol",
            new XElement("li", "empty")
        );

        public Task yield()
        {
            this.SessionwatchReflection = this.Sessionwatch;

            return "ok".ToTaskResult();
        }

        public Task SetTitle()
        {
            head.Element("title").Value = "at " + Sessionwatch.ElapsedMilliseconds;

            return "ok".ToTaskResult();
        }

        public Task ShowTitle()
        {
            this.head.Add(
                new XElement("style", "head, title {display: block;}")
            );

            return "ok".ToTaskResult();
        }

        public Task DoDatabase()
        {
            Console.WriteLine(
                "enter DoDatabase " + new
                    {


                        Sessionwatch = new { Sessionwatch.ElapsedMilliseconds, Sessionwatch.IsRunning }

                    }
            );

            this.output.RemoveNodes();

            new StopwatchTimetravelExperiment.Design.Book1.Sheet1().Insert(
               new Design.Book1Sheet1Row { Sessionwatch = this.Sessionwatch }
           );

            new StopwatchTimetravelExperiment.Design.Book1.Sheet1().SelectAllAsEnumerable().WithEach(
                x =>
                {
                    var li = new XElement("li", new { x.Sessionwatch.ElapsedMilliseconds }.ToString());

                    this.output.Add(li);
                }
            );

            Console.WriteLine(
                new { this.output }
            );

            return "ok".ToTaskResult();
        }

        public Task<TheWatchers> WebMethod2(TheWatchers e)
        {
            // StringConversionsForStopwatch.ConvertFromString { ElapsedMilliseconds = 333, IsRunning = True }
            Console.WriteLine("enter WebMethod2 " + new
                {


                    Watch1 = new { e.Watch1.ElapsedMilliseconds, e.Watch1.IsRunning },
                    Sessionwatch = new { Sessionwatch.ElapsedMilliseconds, Sessionwatch.IsRunning }

                }
            );





            // supported by
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224

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
