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
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace LANXElementExperiment
{
    public enum ApplicationIdentity : long { }

    static class memory
    {
        public static List<xRow> session = new List<xRow>();
    }

    public class xRow
    {

        public int x;
        public int y;

        //public ApplicationIdentity Key = (ApplicationIdentity)new Random().Next();
        public int Key;

    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : xRow
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");




        //public readonly IEnumerable<ApplicationWebService> others;

        // db query should not be launched before MoveNext! for that ToArray is to be used instead
        //public IEnumerable<xRow> others;
        public xRow[] others;


        //public IEnumerable<ApplicationWebService> others;

        //public ApplicationWebService()
        //{
        //    // look, almost like a delegate.
        //    others = from x in session
        //             where x.Key != this.Key
        //             select x;
        //}

        public async Task yield()
        {
            // http://blogs.msdn.com/b/tom/archive/2008/09/18/asp-net-tips-careful-use-of-static-s.aspx
            // static not working anymore the way we expect it to?

            // remove old copies
            //memory.session.RemoveAll(xx => xx.Key == Key);

            others = (
                from xx in memory.session
                where xx.Key != this.Key
                select xx
                ).ToArray();

            memory.session = others.ToList();

            memory.session.Add(
                new xRow { x = x, y = y, Key = Key }
                );



            Header.Value =
                new
            {
                CurrentProcess = Process.GetCurrentProcess().Id,
                CurrentDomain = AppDomain.CurrentDomain.Id,


                // Error	4	The type name 'Current' does not exist in the type 'System.Threading.Tasks.TaskScheduler'	X:\jsc.svn\examples\javascript\io\LANXElementExperiment\LANXElementExperiment\ApplicationWebService.cs	84	47	LANXElementExperiment
                //TaskScheduler = TaskScheduler.Current.Id

                CurrentThread = Thread.CurrentThread.ManagedThreadId,


                Key,
                x,
                y,

                memory.session.Count,
                others  = others.Length
            }.ToString();



        }

    }
}
