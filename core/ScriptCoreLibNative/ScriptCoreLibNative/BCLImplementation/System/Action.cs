using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System
{
	// A native delegate is a static function pointer

	[Script(IsNative = true, Implements = typeof(global::System.Action<>))]
	internal delegate void __Action<T1>(T1 t1);

	[Script(IsNative = true, Implements = typeof(global::System.Action<,,>))]
	internal delegate void __Action<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

	[Script(IsNative = true, Implements = typeof(global::System.Action<,,,>))]
	internal delegate void __Action<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

}
