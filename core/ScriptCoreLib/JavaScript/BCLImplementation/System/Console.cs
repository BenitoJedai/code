using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using System.Media;
using System.IO;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{


    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
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

                WriteIdent();
                WriteLine("<" + _task + ">");

                _ident++;
            }

            void WriteIdent()
            {
                int i = _ident;

                while (i-- > 0)
                    Write(" ");
            }



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
                    InternalDump(Native.window, e);
                }
                catch
                {
                    // X:\jsc.svn\examples\javascript\WebWorkerExperiment\WebWorkerExperiment\Application.cs
                    // web worker wont have direct access to console?
                }
            }

            public static void Write(object e)
            {
                //if (_ci == null)
                Dump("" + e);
                //else
                //    _ci.WriteString(e + "");
            }

            public static void WriteLine(string e)
            {
                Write(e);
                Write("\n");
            }

            public static void WriteLine(object e)
            {
                Write(e);
                Write("\n");
            }

            public void Dispose()
            {
                _ident--;

                var end = IDate.Now - StartTime;

                WriteIdent();
                WriteLine("</" + _task + " - " + end + "ms >");
            }

            #endregion


            public static bool ShowLogAsStatus = false;

            public static void Log(string p)
            {
                if (Native.Document == null)
                    return;

                // browsers dont use status anymore?
                if (ShowLogAsStatus)
                    Native.window.status = p;

                WriteLine(IDate.Now.toLocaleString() + " " + p);
            }

            public static void LogError(string u)
            {
                Log("*** " + u);

            }

            public static void LogError(object u)
            {
                Log("*** " + u.ToString());

            }

            public static void WriteLine()
            {
                WriteLine("");
            }
        }


        public static void WriteLine(object e)
        {
            Out.WriteLine(e);
        }

        public static void WriteLine(string e)
        {
            Out.Write(e + Environment.NewLine);
        }
        public static void WriteLine(Int64 e)
        {
            Out.Write(e + Environment.NewLine);
        }
        public static void WriteLine()
        {
            Out.WriteLine("");
        }

        public static void WriteLine(string e, object x)
        {
            Out.WriteLine(string.Format(e, x));
        }

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
            SystemSounds.Beep.Play();
        }

        #region SetOut
        static global::System.IO.TextWriter InternalOut;
        public static global::System.IO.TextWriter Out
        {
            get
            {
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
            public override void Write(object value)
            {
                __BrowserConsole.Write(value);
            }


            public override void Write(string value)
            {
                __BrowserConsole.Write(value);
            }

            public override void WriteLine(string value)
            {
                __BrowserConsole.WriteLine(value);
            }

            public override void WriteLine(object value)
            {
                __BrowserConsole.WriteLine(value);

            }


            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
        #endregion

    }
}
