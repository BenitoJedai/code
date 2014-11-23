using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Console))]
    internal static class __Console
    {
        // C:\Windows\system32\Macromed\Flash\NPSWF64_15_0_0_189.dll

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Console.cs


        // Create mm.cfg file in this directory:
        //Microsoft Windows Vista
        //C:\Users\user_name\
        //Microsoft Windows 2000/XP
        //C:\Documents and Settings\user_name\

        // C:\Users\arvo\AppData\Roaming\Macromedia\Flash Player\Logs

        // C:\Users\username\mm.cfg
        // http://www.adobe.com/devnet/flashplayer/articles/fplayer9_security_05.html

        // http://www.adobe.com/devnet/flex/articles/client_debug_print.html
        // http://livedocs.adobe.com/flex/201/html/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Book_Parts&file=security2_117_44.html

        // http://livedocs.adobe.com/flex/201/html/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Book_Parts&file=security2_117_44.html
        // http://livedocs.adobe.com/flex/2/langref/package.html#trace()

        // To enable tracing, you must configure the debugger version of Flash Player 
        // as described in Configuring the debugger version of Flash Player to record trace() output.
        // http://www.adobe.com/support/flashplayer/downloads.html

        // http://jpauclair.net/2010/02/10/mmcfg-treasure/
        // TraceOutputFileEnable=1
        // "C:\Users\Arvo\mm.cfg"

        [Script(OptimizedCode = "trace(e);")]
        internal static void trace(string e)
        {
        }


        public static void WriteLine(string e)
        {
            Out.WriteLine(e);
        }



        public static void WriteLine()
        {
            Out.WriteLine("");
        }

        public static void WriteLine(object e)
        {
            if (e == null)
                return;

            Out.WriteLine(e.ToString());
        }

        public static void Write(string e)
        {
            Out.Write(e);
        }

        public static void Write(object e)
        {
            if (e == null)
                return;

            Out.Write(e.ToString());
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
        public class __OutWriter : TextWriter
        {
            public Action<string> AtWriteLine;

            static StringBuilder WriteLinePending = new StringBuilder();


            public override void Write(string value)
            {
                WriteLinePending.Append(value);
            }

            public override void WriteLine(string value)
            {
                var x = WriteLinePending.ToString();

                if (x.Length > 0)
                    WriteLinePending = new StringBuilder();

                var n = x + value;

                // X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsync\AIRThreadedSoundAsync\ApplicationSprite.cs
                if (AtWriteLine != null)
                    AtWriteLine(n);

                trace(n);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
        #endregion

    }
}
