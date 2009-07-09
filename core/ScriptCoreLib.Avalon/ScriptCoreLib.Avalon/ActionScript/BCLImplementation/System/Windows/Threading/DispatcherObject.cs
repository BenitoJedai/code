using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Threading
{
	[Script(Implements = typeof(global::System.Windows.Threading.DispatcherObject))]
	internal class __DispatcherObject
	{
		public __Dispatcher Dispatcher
		{
			get
			{
				return new __Dispatcher();
			}
		}
	}
}
