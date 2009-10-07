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
}
