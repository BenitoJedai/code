using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ExperimentalCompositeFileStream
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            //Error	2	The type arguments for method 'System.Linq.Enumerable.Select<TSource,TResult>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\android\ExperimentalCompositeFileStream\ExperimentalCompositeFileStream\ApplicationWebService.cs	33	22	ExperimentalCompositeFileStream

            var c = new CompositeStream(
                     h.GetFiles().Select(k =>
                         {

                             Console.WriteLine(new { k.Name });


                             return new Func<Stream>(() => (Stream)File.OpenRead(k.Name));
                         }
                )
            );

            var count = 0;
            foreach (var y in c.GetBytes())
            {
                count++;
            }


            h.Context.Response.Write(new { count }.ToString());
            h.CompleteRequest();

            // { count = 1806719 }
        }
    }

    class CompositeStream
    {
        internal readonly IEnumerable<Func<Stream>> s;

        //        [javac] V:\src\ExperimentalCompositeFileStream\CompositeStream__GetBytes_d__0.java:86: __this has private access in ExperimentalCompositeFileStream.CompositeStream__GetBytes_d__0__MoveNext_
        //[javac]         next_0.__this = this;
        //[javac]               ^

        //[javac] location: class ExperimentalCompositeFileStream.CompositeStream__GetBytes_d__0__MoveNext_
        //[javac]     private static _ArrayType_12 _MoveNext__0000__lookup;
        //[javac]                    ^
        //[javac] V:\src\ExperimentalCompositeFileStream\CompositeStream__GetBytes_d__0.java:86: __this has private access in ExperimentalCompositeFileStream.CompositeStream__GetBytes_d__0__MoveNext_
        //[javac]         next_0.__this = this;
        //[javac]               ^
        //[javac] V:\src\ExperimentalCompositeFileStream\CompositeStream__GetBytes_d__0.java:88: ___ has private access in ExperimentalCompositeFileStream.CompositeStream__GetBytes_d__0__MoveNext_
        //[javac]         return next_0.___;
        //[javac]                      ^
        //[javac] V:\src\ExperimentalCompositeFileStream\CompositeStream__GetBytes_d__0.java:211: __loc0 has private access in ExperimentalCompositeFileStream.CompositeStream__GetBytes_d__0__MoveNext_

        public CompositeStream(IEnumerable<Func<Stream>> s)
        {
            this.s = s;
        }

        public IEnumerable<byte> GetBytes()
        {
            Console.WriteLine("enter GetBytes");
            var x = this.s.GetEnumerator();

            while (x.MoveNext())
            {
                var ss = x.Current();

                var z = false;
                do
                {
                    var y = ss.ReadByte();
                    z = y != -1;

                    if (z)
                    {
                        yield return (byte)y;
                    }
                }
                while (z);

                ss.Dispose();
            }
            Console.WriteLine("exit GetBytes");
        }
    }
}
