using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // X:\opensource\github\WootzJs\WootzJs.Runtime\Console.cs
    // http://referencesource.microsoft.com/#mscorlib/system/console.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Console.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Console.cs

    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141223

        //console: WorkerConsole
        //this: ServiceWorkerGlobalScope
        //this.console: WorkerConsole
        //this.console.log("hi"): undefined


        /// <summary>
        /// firefox: run with -console
        /// and about:config must be altered
        /// explorer: activex addin required
        /// 
        /// browser.dom.window.dump.enabled
        /// 
        /// <see>http://kb.mozillazine.org/Viewing_dump%28%29_output</see>
        /// </summary>
        /// 

        public static ConsoleColor ForegroundColor { get; set; }
        public static ConsoleColor BackgroundColor { get; set; }



        #region WriteLine
        public static void WriteLine(object e)
        {
            Out.WriteLine(e);
        }

        public static void WriteLine(string e)
        {
            Out.WriteLine(e);
        }
        public static void WriteLine(long e)
        {
            Out.WriteLine("" + e);
        }

        public static void WriteLine(double e)
        {
            // X:\jsc.svn\examples\javascript\RoslynEndUserPreviewExperiment\RoslynEndUserPreviewExperiment\Application.cs

            Out.WriteLine("" + e);
        }

        public static void WriteLine()
        {
            Out.WriteLine("");
        }

        public static void WriteLine(string e, object x)
        {
            // ?
            Out.WriteLine(string.Format(e, x));
        }
        #endregion




        public static void Write(string e)
        {
            Out.Write(e);
        }

        public static void Write(object e)
        {
            Out.Write(e);
        }





        public void Beep()
        {
            // does it still work? :P
            SystemSounds.Beep.Play();
        }

        // X:\jsc.svn\examples\javascript\VisualConsole\VisualConsole\Application.cs
        public static string Title
        {
            get { return Native.document.title; }
            set { Native.document.title = value; }
        }

        #region SetOut
        static global::System.IO.TextWriter InternalOut;
        public static global::System.IO.TextWriter Out
        {
            get
            {
                // X:\jsc.svn\examples\javascript\VisualConsole\VisualConsole\Application.cs

                if (InternalOut == null)
                    InternalOut = new __OutWriter();

                return InternalOut;
            }
        }

        public static void SetOut(global::System.IO.TextWriter newOut)
        {
            InternalOut = newOut;
        }

        [Script]
        class __OutWriter : TextWriter
        {
            Stopwatch w = Stopwatch.StartNew();

            string href;

            string GetPrefix()
            {
                var ww = "";

                // what about web workers?
                if (Native.document != null)
                {
                    if (href != Native.document.location.href)
                    {
                        w = Stopwatch.StartNew();
                        href = Native.document.location.href;
                    }

                    //w = Native.css
                    // start reporting how many .css rules we have
                    ww = IStyleSheet.all.Rules.Length + ":";

                }

                return ww + w.ElapsedMilliseconds + "ms ";
            }

            public override void Write(object value)
            {
                __BrowserConsole.Write(value);
            }

            public override void Write(string value)
            {
                __BrowserConsole.Write(value);
            }

            long oldRuleCount = 0;
            public override void WriteLine(string value)
            {
                // X:\jsc.svn\examples\javascript\Test\TestImplicitTimelineRecordingEvents\TestImplicitTimelineRecordingEvents\Application.cs


                if (value.StartsWith("event:"))
                {
                    // he console.timeStamp() method only functions while a Timeline recording is in progress.

                    var old = new { Console.BackgroundColor };
                    Console.BackgroundColor = ConsoleColor.Yellow;

                    new IFunction("text", "if (this.console && this.console.timeStamp) this.console.timeStamp(text);").apply(
                         Native.window,
                         value
                     );

                    __BrowserConsole.WriteLine(GetPrefix() + value);
                    Console.BackgroundColor = old.BackgroundColor;

                    return;
                }

                // what about web workers?
                if (Native.document != null)
                {
                    if (IStyleSheet.all.Rules.Length == oldRuleCount)
                    {
                        var old = new { Console.ForegroundColor };
                        Console.ForegroundColor = ConsoleColor.Gray;

                        __BrowserConsole.WriteLine(GetPrefix() + value);
                        Console.ForegroundColor = old.ForegroundColor;


                        return;
                    }
                    oldRuleCount = IStyleSheet.all.Rules.Length;
                }



                __BrowserConsole.WriteLine(GetPrefix() + value);
            }

            public override void WriteLine(object value)
            {
                __BrowserConsole.WriteLine(GetPrefix() + value);

            }


            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
        #endregion

    }




    [Script]
    internal class __BrowserConsole : global::System.IDisposable
    {
        //[Script(InternalConstructor = true)]
        //class ConsoleImplementation : IActiveX
        //{
        //    public bool CloseConsole()
        //    {
        //        return default(bool);
        //    }

        //    public bool OpenConsole()
        //    {
        //        return default(bool);
        //    }

        //    public bool WriteString(string e)
        //    {
        //        return default(bool);
        //    }
        //}

        #region IDisposable Members

        string _task;

        static int _ident;

        //static ConsoleImplementation _ci = null;

        static __BrowserConsole()
        {


        }

        ///// <summary>
        ///// Your internet explorer needs to allow creating the activex object.
        ///// Also you should register the ActiveXConsole.dll
        ///// </summary>
        //public static void EnableActiveXConsole()
        //{
        //    if (_ci == null)
        //    {
        //        _ci = (ConsoleImplementation)new IActiveX("ActiveXConsole.Console");

        //        if (_ci != null)
        //            _ci.OpenConsole();
        //    }
        //}

        double StartTime;

        public __BrowserConsole(string task)
        {
            _task = task;

            StartTime = IDate.Now;

            //WriteIdent();
            WriteLine("<" + _task + ">");

            _ident++;
        }

        //void WriteIdent()
        //{
        //    int i = _ident;

        //    while (i-- > 0)
        //        Write(" ");
        //}



        [Script(OptimizedCode = @"

    if (w0)
    {
            if (w0['dump'] != void(0))
                w0.dump(e0);

			if (w0['console'] != void(0))
                w0.console.log(e0);
    }

            ")]
        internal static void InternalDump(object w0, object e0) { }

        internal static void Dump(object e)
        {
            try
            {
                DumpWithinTry(e);
            }
            catch
            {
                // X:\jsc.svn\examples\javascript\WebWorkerExperiment\WebWorkerExperiment\Application.cs
                // web worker wont have direct access to console?
            }
        }

        private static void DumpWithinTry(object e)
        {
            // Styling console output with CSS
            // https://developers.google.com/chrome-developer-tools/docs/console
            // X:\jsc.svn\examples\javascript\Test\TestConsoleBackground\TestConsoleBackground\Application.cs

            if (Console.BackgroundColor == ConsoleColor.Yellow)
            {
                // +		$exception	{"recursion detected at stack 32"}	System.Exception


                // console.log("%cThis will be formatted with large, blue text", "color: blue; font-size: x-large");

                var args = new object[]{
                             "%c" + e,
                            "background-color: yellow;"
                        };
                new IFunction("text", "css", "this.console.log(text, css);").apply(
                    Native.window,
                    args
                );

                return;
            }

            // why not all colors? :P
            if (Console.ForegroundColor == ConsoleColor.Gray)
            {
                // +		$exception	{"recursion detected at stack 32"}	System.Exception


                // console.log("%cThis will be formatted with large, blue text", "color: blue; font-size: x-large");

                var args = new object[]{
                             "%c" + e,
                            "color: gray;"
                        };
                new IFunction("text", "css", "this.console.log(text, css);").apply(
                    Native.window,
                    args
                );

                return;
            }


            if (Native.serviceworker != null)
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141223
                // X:\jsc.svn\examples\javascript\test\TestServiceWorker\TestServiceWorker\Application.cs
                InternalDump(Native.serviceworker, e);
                return;
            }

            InternalDump(Native.window, e);
        }

        static string __WritePending = "";

        public static void Write(object e)
        {
            // capture console colors?
            __WritePending += e;

            //if (_ci == null)
            //Dump("" + e);
            //else
            //    _ci.WriteString(e + "");
        }

        public static void WriteLine(string e)
        {
            Dump(__WritePending + e);
            __WritePending = "";
        }

        public static void WriteLine(object e)
        {
            WriteLine("" + e);
        }

        public static void WriteLine()
        {
            WriteLine("");
        }

        [Obsolete]
        public void Dispose()
        {
            _ident--;

            var end = IDate.Now - StartTime;

            //WriteIdent();
            WriteLine("</" + _task + " - " + end + "ms >");
        }

        #endregion

        // ?
        public static bool ShowLogAsStatus = false;

        [Obsolete]
        public static void Log(string p)
        {
            if (Native.Document == null)
                return;

            // browsers dont use status anymore?
            if (ShowLogAsStatus)
                Native.window.status = p;

            WriteLine(IDate.Now.toLocaleString() + " " + p);
        }

        [Obsolete]
        public static void LogError(string u)
        {
            Log("*** " + u);

        }

        [Obsolete]
        public static void LogError(object u)
        {
            Log("*** " + u.ToString());

        }


    }

}
