using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// http://referencesource.microsoft.com/#mscorlib/system/intptr.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IntPtr.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System/IntPtr.cs

	// X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\IntPtr.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\IntPtr.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IntPtr.cs

	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/IntPtr.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/IntPtr.cs

	[Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
		// http://www.viva64.com/en/b/0287/

		// X:\jsc.svn\examples\javascript\Test\Test453DefaultIntPtr\Test453DefaultIntPtr\Class1.cs

		// can we share
		// void* between service worker and ui?
		// what about thread hopping?
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401


		// tested by
		// X:\jsc.svn\core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing\JavaScript\BCLImplementation\System\Drawing\Bitmap.cs
		// X:\jsc.svn\examples\javascript\forms\MandelbrotFormsControl\MandelbrotFormsControl\Application.cs

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150104/dynamic

		// usedby?
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
