using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Delegates
{
    [Obsolete]
	public delegate void Async<T, Y>(T t, Action<Y> y);
}
