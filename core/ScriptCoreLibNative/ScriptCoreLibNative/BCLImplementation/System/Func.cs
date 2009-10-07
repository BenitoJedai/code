using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System
{
	// currently jsc supports only singlecast static delegats
	// marked with isnative

	[Script(IsNative = true, Implements = typeof(global::System.Func<>))]
	internal delegate T1 __Func<T1>();

	[Script(IsNative = true, Implements = typeof(global::System.Func<,>))]
	internal delegate T2 __Func<T1, T2>(T1 t1);

	[Script(IsNative = true, Implements = typeof(global::System.Func<,,>))]
	internal delegate T3 __Func<T1, T2, T3>(T1 t1, T2 t2);

	[Script(IsNative = true, Implements = typeof(global::System.Func<,,,>))]
	internal delegate T4 __Func<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3);

	[Script(IsNative = true, Implements = typeof(global::System.Func<,,,,>))]
	internal delegate T5 __Func<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4);

}
