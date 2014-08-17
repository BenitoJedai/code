using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/intptr.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\IntPtr.cs

    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        // tested by
        // X:\jsc.svn\core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing\JavaScript\BCLImplementation\System\Drawing\Bitmap.cs
        // X:\jsc.svn\examples\javascript\forms\MandelbrotFormsControl\MandelbrotFormsControl\Application.cs

        public Uint8ClampedArray PointerToUInt8;

        [Script(OptimizedCode = "return a==b")]
        static public bool operator ==(__IntPtr a, __IntPtr b)
        {
            return default(bool);
        }

        [Script(OptimizedCode = "return a!=b")]
        static public bool operator !=(__IntPtr a, __IntPtr b)
        {
            return default(bool);
        }

        public override bool Equals(object obj)
        {
            return this == (__IntPtr)obj;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}
