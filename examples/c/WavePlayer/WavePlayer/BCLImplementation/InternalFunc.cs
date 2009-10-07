using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer.BCLImplementation
{
	// A native delegate is a static function pointer

	[Script(IsNative = true)]
	internal delegate T1 InternalFunc<T1>();

	[Script(IsNative = true)]
	internal delegate T2 InternalFunc<T1, T2>(T1 t1);

	[Script(IsNative = true)]
	internal delegate T3 InternalFunc<T1, T2, T3>(T1 t1, T2 t2);

	[Script(IsNative = true)]
	internal delegate T4 InternalFunc<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3);

	[Script(IsNative = true)]
	internal delegate T5 InternalFunc<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4);


	[Script(IsNative = true)]
	internal delegate void InternalAction<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

	[Script(IsNative = true)]
	internal delegate void InternalAction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
}
