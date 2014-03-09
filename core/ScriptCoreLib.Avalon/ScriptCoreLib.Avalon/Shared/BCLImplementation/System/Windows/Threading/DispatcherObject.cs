using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Threading
{
	[Script(Implements = typeof(global::System.Windows.Threading.DispatcherObject))]
	public class __DispatcherObject
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
