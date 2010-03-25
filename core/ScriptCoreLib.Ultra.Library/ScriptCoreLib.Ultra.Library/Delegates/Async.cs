using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Delegates
{
	public delegate void Async<T, Y>(T t, Action<Y> y);
}
