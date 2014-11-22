using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // tested by?
    // X:\jsc.svn\examples\c\Test\TestAction\TestAction\Program.cs

	// A native delegate is a static function pointer
    [Script(Implements = typeof(global::System.Threading.ParameterizedThreadStart))]
    internal delegate void __ParameterizedThreadStart(object obj);


}
