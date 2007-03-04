using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.System;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.Runtime
{

    /// <summary>
    /// firefox: run with -console
    /// and about:config must be altered
    /// explorer: activex addin required
    /// 
    /// <see>http://kb.mozillazine.org/Viewing_dump%28%29_output</see>
    /// </summary>
    [Script]
    public class Console : global::System.IDisposable  
    {
        [Script(InternalConstructor = true)]
        class ConsoleImplementation : IActiveX
        {
            public bool CloseConsole()
            {
                return default(bool);
            }

            public bool OpenConsole()
            {
                return default(bool);
            }

            public bool WriteString(string e)
            {
                return default(bool);
            }
        }

        #region IDisposable Members

        string _task;

        static int _ident;

        static ConsoleImplementation _ci = null;

        static Console()
        {


        }

        /// <summary>
        /// Your internet explorer needs to allow creating the activex object.
        /// Also you should register the ActiveXConsole.dll
        /// </summary>
        public static void EnableActiveXConsole()
        {
            if (_ci == null)
            {
                _ci = (ConsoleImplementation)new IActiveX("ActiveXConsole.Console");

                if (_ci != null)
                    _ci.OpenConsole();
            }
        }

        double StartTime;

        public Console(string task)
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
            if (w0['dump'] != void(0))
                w0.dump(e0);
            ")]
        internal static void InternalDump(object w0, object e0) { }

        internal static void Dump(object e)
        {
           
                InternalDump(Native.Window, e);

        }

        public static void Write(object e)
        {
            if (_ci == null)
                Dump(e);
            else
                _ci.WriteString(e + "");
        }

        public static void WriteLine(string e)
        {
            Write(e);
            Write("\n");
        }

        public void Dispose()
        {
            _ident--;

            var end = IDate.Now - StartTime;

            WriteIdent();
            WriteLine("</" + _task +  " - " +end + "ms >");
        }

        #endregion

        public static void LogAssambley(params IAssemblyInfo[] e)
        {
            if (Native.Document == null)
                return;

            var max = Helper.Max(e, 0, ( p) => p.TargetOut = p.TargetIn.ModuleName.Length);

            Console.Log(" Assembly list :".PadLeft(max + 2, '-'));

            foreach (IAssemblyInfo a in e)
            {
                Console.Log(a.ModuleName.PadRight(max) + " : " + a.BuildDateTimeString);
            }
        }

        public static bool ShowLogAsStatus = false;

        public static void Log(string p)
        {
            if (Native.Document == null)
                return;

            if (ShowLogAsStatus)
                Native.Window.status = p;

            WriteLine(IDate.Now.toLocaleString() + " " + p);
        }

        public static void LogError(string u)
        {
            Log("*** " + u);

        }

        public static void WriteLine()
        {
            WriteLine("");
        }
    }
}
