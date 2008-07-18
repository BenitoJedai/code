using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    
    [Script(Implements = typeof(global::System.Console))]
    internal static class __Console
    {
        // http://livedocs.adobe.com/flex/201/html/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Book_Parts&file=security2_117_44.html
        // http://livedocs.adobe.com/flex/2/langref/package.html#trace()
        [Script(OptimizedCode = "trace(e);")]
        internal static void trace(string e)
        {
        }

        public static void WriteLine(string e)
        {
            trace(e);
        }

        public static void WriteLine()
        {
            WriteLine("");
        }

        public static void WriteLine(object e)
        {
            if (e == null)
                return;

            WriteLine(e.ToString());
        }

        public static void Write(string e)
        {
            trace(e);
        }

        public static void Write(object e)
        {
            if (e == null)
                return;

            Write(e.ToString());
        }
    }
}
