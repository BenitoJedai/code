using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
	[Script(Implements = typeof(global::System.Threading.AutoResetEvent))]
    internal class __AutoResetEvent : __WaitHandle
	{
        public __AutoResetEvent(bool initialState)
        {
        }
	}
}
