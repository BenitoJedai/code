using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ScriptCoreLib.CompilerServices;

namespace ScriptCoreLib.Extensions
{
    public static class THREEExtensions
    {
        public static void setSize(this THREE.WebGLRenderer r, IWindow w = null)
        {
            if (w == null)
                w = Native.window;

            r.setSize(w.Width, w.Height);

        }

        // x:\jsc.svn\core\scriptcorelib.async\scriptcorelib.async\compilerservices\callerfilelineattribute.cs
        public static T AttachTo<T>(this T r,
            THREE.Object3D c,

                // https://msdn.microsoft.com/en-us/library/hh534540.aspx
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0,
                [CallerFileLine] string sourceFileLine = ""
            ) where T : THREE.Object3D
        {
            // what about caller IL hints?

            // x:\jsc.svn\core\scriptcorelib.windows.forms\scriptcorelib.windows.forms\javascript\bclimplementation\system\windows\forms\treeview\treenode.cs
            // x:\jsc.svn\examples\javascript\webgl\collada\webglrah66comanche\webglrah66comanche\library\zeproperties.cs

            // a hint for out of band code patching/roslyn code analyzer
            //r.name = "\{sourceFilePath}:\{sourceLineNumber} \{sourceFileLine}";
            r.userData = new CallerFileLineAttribute
            {
                sourceFilePath = sourceFilePath,
                sourceLineNumber = sourceLineNumber,
                sourceFileLine = sourceFileLine
            };


            // can we guess our name?
            // would be nice if we had some roslyn baked analysis on the this pointer
            r.name = sourceFileLine.Trim().TakeUntilOrEmpty(".AttachTo(");

            c.add(r);

            return r;
        }
    }
}
