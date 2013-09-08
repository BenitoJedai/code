using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //[javac] Compiling 486 source files to V:\bin\classes
        //[javac] V:\src\ScriptCoreLib\Ultra\WebService\InternalGlobalExtensions_CompositeStream__GetBytes_d__2a.java:327: s has private access in ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions_CompositeStream
        //[javac]         _arg0.__this._x_5__2b = _arg0.__this.__4__this.s.System_Collections_Generic_IEnumerable_1_GetEnumerator();
        //[javac]                                                       ^
        //[javac] Note: Some input files use or override a deprecated API.
        //[javac] Note: Recompile with -Xlint:deprecation for details.


        //[javac] Compiling 528 source files to V:\bin\classes
        //[javac] V:\src\ExperimentalCompositeFileStream\CompositeStream__GetBytes_d__0__MoveNext_.java:14: cannot find symbol
        //[javac] symbol  : class _ArrayType_12
        //[javac] location: class ExperimentalCompositeFileStream.CompositeStream__GetBytes_d__0__MoveNext_
        //[javac]     private static _ArrayType_12 _MoveNext__0000__lookup;
        //[javac]                    ^
        //[javac] V:\src\ExperimentalCompositeFileStream\CompositeStream__GetBytes_d__0.java:327: s has private access in ExperimentalCompositeFileStream.CompositeStream
        //[javac]         _arg0.__this._x_5__1 = _arg0.__this.__4__this.s.System_Collections_Generic_IEnumerable_1_GetEnumerator();
        //[javac]                                                      ^
        //[javac] V:\src\ScriptCoreLib\Ultra\WebService\InternalGlobalExtensions_CompositeStream__GetBytes_d__2a.java:327: s has private access in ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions_CompositeStream
        //[javac]         _arg0.__this._x_5__2b = _arg0.__this.__4__this.s.System_Collections_Generic_IEnumerable_1_GetEnumerator();
        //[javac]                                                       ^
        //[javac] Note: Some input files use or override a deprecated API.
        //[javac] Note: Recompile with -Xlint:deprecation for details.

        public static object HandlerSync = new object();

        //[MethodImplAttributes]
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            Console.WriteLine("enter Handler");

            //script: error JSC1000: Java :
            // BCL needs another method, please define it.
            // Cannot call type without script attribute :
            // System.Threading.Monitor for Void Enter(System.Object, Boolean ByRef) used at
            // ExperimentalCompositeFileStream.ApplicationWebService.Handler at offset 0018.
            // If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.
            //System.InvalidOperationException: Java :
            // BCL needs another method, please define it.

            //lock (HandlerSync)
            {
                Console.WriteLine("enter Handler lock");
                //Error	2	The type arguments for method 'System.Linq.Enumerable.Select<TSource,TResult>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\android\ExperimentalCompositeFileStream\ExperimentalCompositeFileStream\ApplicationWebService.cs	33	22	ExperimentalCompositeFileStream

                //enter GetBytes
                //{ Name = ExperimentalCompositeFileStream.Application.exe.js }
                //{ Name = ScriptCoreLib.dll.js }
                //{ Name = Application.htm }
                //{ Name = assets/ExperimentalCompositeFileStream/App.css }
                //{ Name = assets/ScriptCoreLib/jsc.ico }
                //{ Name = assets/ScriptCoreLib/jsc.png }
                //{ Name = assets/ScriptCoreLib/loading.gif }
                //{ Name = assets/ScriptCoreLib/Windows Ding.wav }
                //{ Name = ExperimentalCompositeFileStream.Application.exe.jsgz }
                //exit GetBytes

                var e = new Stopwatch();
                e.Start();


                var c = new XCompositeStream(
                         h.GetFiles().Select(k =>
                             {

                                 Console.WriteLine(new { k.Name });


                                 return new Func<Stream>(
                                     () =>
                                     {
                                         Console.WriteLine(new { k.Name });
                                         return (Stream)File.OpenRead(k.Name);
                                     }
                                 );
                             }
                    )
                );

                var count = 0;
                var buffer = new byte[1024 * 40];

                foreach (var y in c.GetBytes(buffer))
                {

                    //if (count % (1024 * 40) == 0)
                    //{
                    //    //I/System.Console(12919): Caused by: java.lang.NullPointerException
                    //    //I/System.Console(12919):        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream.Flush(__NetworkStream.java:58)

                    count += y;

                    //                    I/System.Console(15969): before ReadByte { Length = 40960 }
                    //I/System.Console(15969): after ReadByte { y = 40960 }
                    //I/System.Console(15969): { count = 0 }
                    //I/System.Console(15969): before ReadByte { Length = 40960 }
                    //I/System.Console(15969): after ReadByte { y = 19844 }
                    //I/System.Console(15969): { count = 0 }
                    //I/System.Console(15969): before ReadByte { Length = 40960 }
                    //I/System.Console(15969): after ReadByte { y = -1 }
                    //I/System.Console(15969): before dispose

                    Console.WriteLine(new { count });
                    //}

                }

                // 26sec
                h.Context.Response.Write(new { count, e.ElapsedMilliseconds }.ToString());
                h.CompleteRequest();
            }

            // { count = 1806719 }
        }
    }

    public class XCompositeStream
    {
        public readonly IEnumerable<Func<Stream>> s;

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

        public XCompositeStream(IEnumerable<Func<Stream>> s)
        {
            this.s = s;
        }

        public IEnumerable<byte> GetBytes()
        {
            //I/System.Console(13581): <0156>
            //I/System.Console(13581): { count = 766225 }
            //I/System.Console(13581): <0000>
            //I/System.Console(13581): <0017>
            //I/System.Console(13581): <00f0>
            //I/System.Console(13581): after yield
            //I/System.Console(13581): <0103>
            //I/System.Console(13581): before re do
            //I/System.Console(13581): <0090>
            //I/System.Console(13581): before ReadByte
            //I/System.Console(13581): <00cc>
            //I/System.Console(13581): before yield
            //I/System.Console(13581): <0156>
            //I/System.Console(13581): { count = 766226 }
            //I/System.Console(13581): <0000>
            //I/System.Console(13581): <0017>
            //I/System.Console(13581): <00f0>
            //I/System.Console(13581): after yield
            //I/System.Console(13581): <0103>
            //I/System.Console(13581): before re do
            //I/System.Console(13581): <0090>
            //I/System.Console(13581): before ReadByte
            //I/System.Console(13581): <00cc>
            //I/System.Console(13581): before yield
            //I/System.Console(13581): <0156>
            //I/System.Console(13581): { count = 766227 }
            //I/System.Console(13581): <0000>
            //I/System.Console(13581): <0017>
            //I/System.Console(13581): <00f0>
            //I/System.Console(13581): after yield
            //I/System.Console(13581): <0103>
            //I/System.Console(13581): before re do
            //I/System.Console(13581): <0090>
            //I/System.Console(13581): before ReadByte
            //I/System.Console(13581): <00cc>
            //I/System.Console(13581): before yield
            //I/System.Console(13581): <0156>
            //I/System.Console(13581): { count = 766228 }
            //I/System.Console(13581): <0000>
            //I/System.Console(13581): <0017>
            //I/System.Console(13581): <00f0>
            //I/System.Console(13581): after yield
            //I/System.Console(13581): <0103>
            //I/System.Console(13581): before re do
            //I/System.Console(13581): <0090>
            //I/System.Console(13581): before ReadByte
            //I/System.Console(13581): <00cc>
            //I/System.Console(13581): before yield
            //I/System.Console(13581): <0156>
            //I/System.Console(13581): { count = 766229 }
            //I/System.Console(13581): <0000>
            //I/System.Console(13581): <0017>
            //I/System.Console(13581): <00f0>
            //I/System.Console(13581): after yield
            //I/System.Console(13581): <0103>
            //I/System.Console(13581): before re do
            //I/System.Console(13581): <0090>
            //I/System.Console(13581): before ReadByte
            //I/System.Console(13581): <00cc>
            //I/System.Console(13581): before yield
            //I/System.Console(13581): <0156>
            //I/System.Console(13581): { count = 766230 }
            //I/System.Console(13581): <0000>
            //I/System.Console(13581): <0017>
            //I/System.Console(13581): <00f0>
            //I/System.Console(13581): after yield
            //I/System.Console(13581): <0103>
            //I/System.Console(13581): before re do
            //I/System.Console(13581): <0090>


            Console.WriteLine("enter GetBytes");
            var x = this.s.GetEnumerator();
            Console.WriteLine("before MoveNext");
            while (x.MoveNext())
            {
                Console.WriteLine("before Current");
                var ss = x.Current();

                var z = false;
                Console.WriteLine("before do");
                do
                {
                    //Console.WriteLine("before ReadByte");
                    var y = ss.ReadByte();
                    z = y != -1;

                    if (z)
                    {
                        //Console.WriteLine("before yield");
                        yield return (byte)y;
                        //Console.WriteLine("after yield");
                    }
                    //Console.WriteLine("before re do");
                }
                while (z);

                Console.WriteLine("before dispose");
                ss.Dispose();
            }
            Console.WriteLine("exit GetBytes");
        }

        public IEnumerable<int> GetBytes(byte[] buffer)
        {
            Console.WriteLine("enter GetBytes");
            var x = this.s.GetEnumerator();
            Console.WriteLine("before MoveNext");
            while (x.MoveNext())
            {
                Console.WriteLine("before Current");
                var ss = x.Current();

                var z = false;
                Console.WriteLine("before do");
                do
                {
                    Console.WriteLine("before ReadByte " + new { buffer.Length });
                    var y = ss.Read(buffer, 0, buffer.Length);
                    Console.WriteLine("after ReadByte " + new { y });
                    z = y != -1;

                    if (z)
                    {
                        //Console.WriteLine("before yield");
                        yield return y;
                        //Console.WriteLine("after yield");
                    }
                    //Console.WriteLine("before re do");
                }
                while (z);

                Console.WriteLine("before dispose");
                ss.Dispose();
            }
            Console.WriteLine("exit GetBytes");
        }
    }
}
